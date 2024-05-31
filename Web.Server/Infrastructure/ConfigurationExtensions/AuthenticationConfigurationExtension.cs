using System.Security.Claims;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;
using Havit.NewProjectTemplate.Web.Client;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public static class AuthenticationConfigurationExtension
{
	public const string MsOidcScheme = "MicrosoftOidc";

	public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		AzureAdOptions azureAdOptions = configuration.GetSection(AzureAdOptions.Path).Get<AzureAdOptions>();

		// Add services to the container.
		services.AddAuthentication(MsOidcScheme)
			.AddOpenIdConnect(MsOidcScheme, oidcOptions =>
			{
				// For the following OIDC settings, any line that's commented out
				// represents a DEFAULT setting. If you adopt the default, you can
				// remove the line if you wish.

				// ........................................................................
				// The OIDC handler must use a sign-in scheme capable of persisting 
				// user credentials across requests.

				oidcOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				// ........................................................................

				// ........................................................................
				// The "openid" and "profile" scopes are required for the OIDC handler 
				// and included by default. You should enable these scopes here if scopes 
				// are provided by "Authentication:Schemes:MicrosoftOidc:Scope" 
				// configuration because configuration may overwrite the scopes collection.

				//oidcOptions.Scope.Add(OpenIdConnectScope.OpenIdProfile);
				// ........................................................................

				// ........................................................................
				// SaveTokens is set to false by default because tokens aren't required
				// by the app to make additional external API requests.

				//oidcOptions.SaveTokens = false;
				// ........................................................................

				// ........................................................................
				// The following paths must match the redirect and post logout redirect 
				// paths configured when registering the application with the OIDC provider. 
				// For Microsoft Entra ID, this is accomplished through the "Authentication" 
				// blade of the application's registration in the Azure portal. Both the
				// signin and signout paths must be registered as Redirect URIs. The default 
				// values are "/signin-oidc" and "/signout-callback-oidc".
				// Microsoft Identity currently only redirects back to the 
				// SignedOutCallbackPath if authority is 
				// https://login.microsoftonline.com/{TENANT ID}/v2.0/ as it is above. 
				// You can use the "common" authority instead, and logout redirects back to 
				// the Blazor app. For more information, see 
				// https://github.com/AzureAD/microsoft-authentication-library-for-js/issues/5783

				//oidcOptions.CallbackPath = new PathString("/signin-oidc");
				//oidcOptions.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
				// ........................................................................

				// ........................................................................
				// The RemoteSignOutPath is the "Front-channel logout URL" for remote single 
				// sign-out. The default value is "/signout-oidc".

				//oidcOptions.RemoteSignOutPath = new PathString("/signout-oidc");
				// ........................................................................

				// ........................................................................
				// The "offline_access" scope is required for the refresh token.

				oidcOptions.Scope.Add(OpenIdConnectScope.OfflineAccess);
				// ........................................................................

				// ........................................................................
				// The following example Authority is configured for Microsoft Entra ID
				// and a single-tenant application registration. Set the {TENANT ID} 
				// placeholder to the Tenant ID. The "common" Authority 
				// https://login.microsoftonline.com/common/v2.0/ should be used 
				// for multi-tenant apps. You can also use the "common" Authority for 
				// single-tenant apps, but it requires a custom IssuerValidator as shown 
				// in the comments below. 

				//oidcOptions.Authority = "https://login.microsoftonline.com/{TENANT ID}/v2.0/";
				oidcOptions.Authority = azureAdOptions.GetAutorityUrl();
				// ........................................................................

				// ........................................................................
				// Set the Client ID for the app. Set the {CLIENT ID} placeholder to
				// the Client ID.

				oidcOptions.ClientId = azureAdOptions.ClientId;

				// ........................................................................

				// ........................................................................
				// JK: Scope s ClientId resolves the issue on Azure AD B2C: OpenIdConnectProtocolException: IDX21336: Both 'id_token' and 'access_token' should be present in OpenIdConnectProtocolValidationContext.ProtocolMessage received from Token Endpoint. Cannot process the message.
				// Source: https://github.com/dotnet/aspnetcore/issues/23284#issuecomment-648775392
				// oidcOptions.Scope.Add(oidcOptions.ClientId);
				// ........................................................................

				// ........................................................................
				// ClientSecret shouldn't be compiled into the application assembly or 
				// checked into source control. Instead adopt User Secrets, Azure KeyVault, 
				// or an environment variable to supply the value. Authentication scheme 
				// configuration is automatically read from 
				// "Authentication:Schemes:{SchemeName}:{PropertyName}", so ClientSecret is 
				// for OIDC configuration is automatically read from 
				// "Authentication:Schemes:MicrosoftOidc:ClientSecret" configuration.

				oidcOptions.ClientSecret = azureAdOptions.ClientSecret;
				// ........................................................................

				// ........................................................................
				// Setting ResponseType to "code" configures the OIDC handler to use 
				// authorization code flow. Implicit grants and hybrid flows are unnecessary
				// in this mode. In a Microsoft Entra ID app registration, you don't need to 
				// select either box for the authorization endpoint to return access tokens 
				// or ID tokens. The OIDC handler automatically requests the appropriate 
				// tokens using the code returned from the authorization endpoint.

				oidcOptions.ResponseType = OpenIdConnectResponseType.Code;
				// ........................................................................

				// ........................................................................
				// Many OIDC servers use "name" and "role" rather than the SOAP/WS-Fed 
				// defaults in ClaimTypes. If you don't use ClaimTypes, mapping inbound 
				// claims to ASP.NET Core's ClaimTypes isn't necessary.

				oidcOptions.MapInboundClaims = false;
				oidcOptions.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
				oidcOptions.TokenValidationParameters.RoleClaimType = "role";
				// ........................................................................

				// ........................................................................
				// Many OIDC providers work with the default issuer validator, but the
				// configuration must account for the issuer parameterized with "{TENANT ID}" 
				// returned by the "common" endpoint's /.well-known/openid-configuration
				// For more information, see
				// https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/1731

				//var microsoftIssuerValidator = AadIssuerValidator.GetAadIssuerValidator(oidcOptions.Authority);
				//oidcOptions.TokenValidationParameters.IssuerValidator = microsoftIssuerValidator.Validate;
				// ........................................................................

				oidcOptions.Events.OnRemoteFailure = HandleRemoteFailureAsync;
			})
			.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			{
				options.AccessDeniedPath = NavigationRoutes.Errors.AccessDenied; // Blazor page

				// Customize claims for the identity "held in the cookie".
				options.Events.OnSigningIn = HandleSigningInAsync;
			});

		// This attaches a cookie OnValidatePrincipal callback to get a new access token when the current one expires, and
		// reissue a cookie with the new access token saved inside. If the refresh fails, the user will be signed out.
		services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, MsOidcScheme);

		return services;
	}

	/// <summary>
	/// In case of a remote failure during the authentication process, handle the exception and redirect the user to the homepage.
	/// </summary>
	private static Task HandleRemoteFailureAsync(RemoteFailureContext context)
	{
		// Source: https://github.com/openiddict/openiddict-core/issues/334#issuecomment-273487558
		context.Response.Redirect("/");
		context.HandleResponse();

		return Task.FromResult(0);
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
			ClaimsIdentity customClaimsIdentity = new ClaimsIdentity();
			customClaimsIdentity.AddClaims(customClaims);
			principal.AddIdentity(customClaimsIdentity);
		}

		return principal;
	}
}
