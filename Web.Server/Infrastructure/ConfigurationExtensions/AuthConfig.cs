using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Identity;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public static class AuthConfig
{
	public static void AddCustomizedAuth(this IServiceCollection services, IConfiguration configuration)
	{
		// Authentication, Authorization, Identity
		services.AddAuthentication(options =>
		{
			options.DefaultScheme = IdentityConstants.ApplicationScheme;
			options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
		})
			.AddIdentityServerJwt()
			.AddIdentityCookies();

		services.ConfigureApplicationCookie(configuration =>
		{
			// Do not redirect API calls - customization to include gRPC calls
			// https://github.com/dotnet/aspnetcore/blob/main/src/Security/Authentication/Cookies/src/CookieAuthenticationEvents.cs
			configuration.Events.OnRedirectToLogin = (context) => RedirectOrApiStatus(context, HttpStatusCode.Unauthorized);
			configuration.Events.OnRedirectToAccessDenied = (context) => RedirectOrApiStatus(context, HttpStatusCode.Forbidden);
		});

		services.AddIdentityCore<User>(options =>
		{
			options.Stores.MaxLengthForKeys = 128;
			options.SignIn.RequireConfirmedAccount = true;
		})
			.AddDefaultUI()
			.AddDefaultTokenProviders()
			.AddRoles<Role>()
			.AddUserStore<UserStore>()
			.AddRoleStore<RoleStore>();

		services.AddIdentityServer()
			.AddAspNetIdentity<User>()
			.AddClients()
			.AddSigningCredentials()
			.AddIdentityResources()
			.AddApiResources()
			.AddProfileService<IdentityServerProfileService>();

		services.PostConfigure<ApiAuthorizationOptions>(options =>
		{
			options.Clients["Havit.NewProjectTemplate.Web.Client"].AlwaysIncludeUserClaimsInIdToken = true;
			options.IdentityResources["openid"].UserClaims.Add("name");
			options.ApiResources.Single().UserClaims.Add("name");
			options.IdentityResources["openid"].UserClaims.Add("role");
			options.ApiResources.Single().UserClaims.Add("role");
		});


		// server-side support for User.IsInRole(), see https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/
		// https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-identity-server?view=aspnetcore-5.0&tabs=visual-studio#api-authorization-options
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
		services.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();
	}

	private static Task RedirectOrApiStatus(RedirectContext<CookieAuthenticationOptions> context, HttpStatusCode apiStatus)
	{
		if (IsApiRequest(context.Request))
		{
			context.Response.Headers[HeaderNames.Location] = context.RedirectUri;
			context.Response.StatusCode = (int)apiStatus;
		}
		else
		{
			context.Response.Redirect(context.RedirectUri);
		}
		return Task.CompletedTask;
	}

	private static bool IsApiRequest(HttpRequest request)
	{
		return
			request.Headers[HeaderNames.ContentType].ToString().StartsWith("application/grpc", StringComparison.Ordinal) // gRPC-Web
			|| String.Equals(request.Query[HeaderNames.XRequestedWith], "XMLHttpRequest", StringComparison.Ordinal) // AJAX
			|| String.Equals(request.Headers[HeaderNames.XRequestedWith], "XMLHttpRequest", StringComparison.Ordinal); // AJAX
	}
}
