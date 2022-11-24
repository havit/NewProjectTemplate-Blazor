using System.IdentityModel.Tokens.Jwt;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public static class AuthConfig
{
	public static void AddCustomizedAuth(this IServiceCollection services, IConfiguration configuration)
	{
		// https://github.com/AzureAD/microsoft-identity-web/wiki/Mixing-web-app-and-web-api-in-the-same-ASP.NET-core-app
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				  .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));
		//.EnableTokenAcquisitionToCallDownstreamApi();

		services.AddAuthentication()
					.AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"), OpenIdConnectDefaults.AuthenticationScheme);
		//.EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
		//	.AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
		//	.AddInMemoryTokenCaches();

		services.Configure<JwtBearerOptions>(options =>
		{
			options.TokenValidationParameters.NameClaimType = "name";
		});

		// server-side support for User.IsInRole(), see https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

		services.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();
		services.AddScoped<IUserContextInfoBuilder, UserContextInfoBuilder>();
	}
}
