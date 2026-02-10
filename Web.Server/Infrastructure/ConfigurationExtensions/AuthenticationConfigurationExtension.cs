using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;
using Havit.NewProjectTemplate.Web.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.Distributed;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

// see https://learn.microsoft.com/en-us/aspnet/core/blazor/security/blazor-web-app-with-entra?view=aspnetcore-10.0

public static class AuthenticationConfigurationExtension
{
	public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		Contract.Assert(services.Any(item => item.ServiceType == typeof(IDistributedCache)), "Prerequisite: Expecting IDistributedCache is already registered (otherwise AddDistributedTokenCaches adds in-memory implementation).");
		Contract.Assert(services.Any(item => item.ServiceType == typeof(IDataProtectionProvider)), "Prerequisite: Expecting IDataProtector is already registered (otherwise no token encryption is performed and the Enctypt=true is silently ignored).");

		AzureAdOptions azureAdOptions = configuration.GetSection(AzureAdOptions.Path).Get<AzureAdOptions>();

		// Add services to the container.
		services
			.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
			.AddMicrosoftIdentityWebApp(microsoftIdentityOptions =>
				{
					microsoftIdentityOptions.TenantId = azureAdOptions.TenantId;
					microsoftIdentityOptions.ClientId = azureAdOptions.ClientId;
					microsoftIdentityOptions.ClientSecret = azureAdOptions.ClientSecret;
					microsoftIdentityOptions.Instance = azureAdOptions.Instance;
					microsoftIdentityOptions.Authority = azureAdOptions.GetAutorityUrl();

					microsoftIdentityOptions.ResponseType = OpenIdConnectResponseType.Code;

					microsoftIdentityOptions.MapInboundClaims = false;

					microsoftIdentityOptions.Events.OnRemoteFailure = HandleRemoteFailureAsync;

					// Handle API requests by returning 401 instead of redirecting to login
					microsoftIdentityOptions.Events.OnRedirectToIdentityProvider = HandleRedirectToIdentityProviderAsync;
				},
				(CookieAuthenticationOptions cookieAuthenticationOptions) =>
				{
					cookieAuthenticationOptions.AccessDeniedPath = NavigationRoutes.Errors.AccessDenied; // Blazor page

					// Customize claims for the identity "held in the cookie".
					cookieAuthenticationOptions.Events.OnSigningIn = HandleSigningInAsync;

					// Handle Access Denied for API requests by returning 403 instead of redirecting
					cookieAuthenticationOptions.Events.OnRedirectToAccessDenied = HandleRedirectToAccessDeniedAsync;
				})
			.EnableTokenAcquisitionToCallDownstreamApi()
			.AddDistributedTokenCaches();

		// EnableTokenAcquisitionToCallDownstreamApi overrides set TokenValidationParameters.NameClaimType
		// we must override it again to ensure that the NameClaimType is set to JwtRegisteredClaimNames.Name
		services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, oidcOptions =>
		{
			oidcOptions.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
			oidcOptions.TokenValidationParameters.RoleClaimType = "role";
		});

		services.Configure<MsalDistributedTokenCacheAdapterOptions>(options =>
		{
			options.Encrypt = true; // requires ASP.NET Data Protection to be registered (otherwise silently ignored)

			options.L1CacheOptions.SizeLimit = 20 * 1024 * 1024; // 20 MB (default: 500 MB), good enough for 1000+ tokens
			options.SlidingExpiration = TimeSpan.FromHours(2); // expiration should be set but not shorter than token lifetime
		});

		return services;
	}

	/// <summary>
	/// In case of interruption during login, an exception occurs. This method handles the exception and redirects the user to the homepage.
	/// </summary>
	private static Task HandleRemoteFailureAsync(RemoteFailureContext context)
	{
		// source: https://github.com/openiddict/openiddict-core/issues/334#issuecomment-273487558
		context.Response.Redirect("/");
		context.HandleResponse();

		return Task.FromResult(0);
	}

	/// <summary>
	/// Handles redirect to identity provider for OIDC authentication.
	/// For API/gRPC requests, returns HTTP 401 instead of redirecting.
	/// For browser requests, performs the normal redirect to identity provider.
	/// </summary>
	private static Task HandleRedirectToIdentityProviderAsync(RedirectContext context)
	{
		var request = context.Request;

		// Check if this is an API request (gRPC, JSON API, etc.)
		if (IsApiRequest(request))
		{
			// Return 401 for API requests instead of redirecting
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.HandleResponse(); // Prevent further processing
			return Task.CompletedTask;
		}

		// For browser requests, let the default behavior handle the redirect
		return Task.CompletedTask;
	}

	// Source: https://stackoverflow.com/questions/51314443/why-doesnt-claims-transformation-reduce-the-cookie-size
	private static async Task<ClaimsPrincipal> HandleSigningInAsync(CookieSigningInContext context)
	{
		// We would like to react to the "remember me" checkbox by issuing a cookie with a longer expiration time.
		// Unfortunately, we don't receive any information to determine the use of "remember me".
		ICustomClaimsBuilder customClaimsBuilder = context.Request.HttpContext.RequestServices.GetRequiredService<ICustomClaimsBuilder>();
		ClaimsPrincipal principal = context.Principal;
		List<Claim> customClaims = await customClaimsBuilder.GetCustomClaimsAsync(principal);

		if (customClaims.Any())
		{
			ClaimsIdentity identity = principal.Identities.First();
			ClaimsIdentity customClaimsIdentity = new ClaimsIdentity(customClaims,
				authenticationType: nameof(CustomClaimsBuilder),
				nameType: identity.NameClaimType,
				roleType: identity.RoleClaimType);
			principal.AddIdentity(customClaimsIdentity);
		}

		return principal;
	}

	/// <summary>
	/// Handles redirect to access denied page.
	/// For API/gRPC requests, returns HTTP 403 instead of redirecting.
	/// For browser requests, performs the normal redirect to access denied page.
	/// </summary>
	private static Task HandleRedirectToAccessDeniedAsync(RedirectContext<CookieAuthenticationOptions> context)
	{
		var request = context.Request;

		// Check if this is an API request (gRPC, JSON API, etc.)
		if (IsApiRequest(request))
		{
			// Return 403 for API requests instead of redirecting
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			return Task.CompletedTask;
		}

		// For browser requests, perform the default redirect
		context.Response.Redirect(context.RedirectUri);
		return Task.CompletedTask;
	}

	/// <summary>
	/// Determines if the request is an API request that should receive 401 instead of redirect.
	/// </summary>
	private static bool IsApiRequest(HttpRequest request)
	{
		// gRPC requests
		if (request.ContentType?.StartsWith("application/grpc") == true)
		{
			return true;
		}

		// SignalR requests
		if (request.Path.StartsWithSegments("/SignalR"))
		{
			return true;
		}

		// Check Accept header for API content types
		var acceptHeader = request.Headers.Accept.ToString();
		if (acceptHeader.Contains("application/json") ||
			acceptHeader.Contains("application/grpc") ||
			acceptHeader.Contains("application/protobuf"))
		{
			return true;
		}

		return false;
	}
}
