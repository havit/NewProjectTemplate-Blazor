using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

// zdroj: https://github.com/dotnet/blazor-samples/tree/main/8.0/BlazorWebAppOidc

internal static partial class CookieOidcServiceCollectionExtensions
{
	public static IServiceCollection ConfigureCookieOidcRefresh(this IServiceCollection services, string cookieScheme, string oidcScheme)
	{
		services.AddSingleton<CookieOidcRefresher>();
		services.AddOptions<CookieAuthenticationOptions>(cookieScheme).Configure<CookieOidcRefresher>((cookieOptions, refresher) =>
		{
			cookieOptions.Events.OnValidatePrincipal = context => refresher.ValidateOrRefreshCookieAsync(context, oidcScheme);
		});
		services.AddOptions<OpenIdConnectOptions>(oidcScheme).Configure(oidcOptions =>
		{
			// Request a refresh_token.
			oidcOptions.Scope.Add("offline_access");
			// Store the refresh_token.
			oidcOptions.SaveTokens = true;
		});
		return services;
	}
}
