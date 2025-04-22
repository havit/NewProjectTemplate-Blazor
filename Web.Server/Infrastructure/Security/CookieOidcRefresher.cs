using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Havit.NewProjectTemplate.Contracts.Infrastructure.Security;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;
using Havit.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

// Source: https://github.com/dotnet/blazor-samples/tree/main/8.0/BlazorWebAppOidc

internal sealed class CookieOidcRefresher(
	IRefreshTokenEndpointResponseCacheStore refreshTokenEndpointResponseCacheStore,
	IOptionsMonitor<OpenIdConnectOptions> oidcOptionsMonitor)
{
	// Static - we need to share the instance of the critical section across all requests.
	private static readonly CriticalSection<string> s_criticalSection = new();

	private readonly OpenIdConnectProtocolValidator oidcTokenValidator = new()
	{
		// We no longer have the original nonce cookie which is deleted at the end of the authorization code flow having served its purpose.
		// Even if we had the nonce, it's likely expired. It's not intended for refresh requests. Otherwise, we'd use oidcOptions.ProtocolValidator.
		RequireNonce = false,
	};

	/// <remarks>
	/// This implementation assumes that only one instance of the application is running.
	/// If we have multiple instances (production environment) then we can configure refresh tokens to be reusable
	/// as multiple instances could be hit with parallel requests where one would consume the refresh token and the others would fail.
	/// </remarks>
	public async Task ValidateOrRefreshCookieAsync(CookieValidatePrincipalContext validateContext, string oidcScheme)
	{
		var accessTokenExpirationText = validateContext.Properties.GetTokenValue("expires_at");
		if (!DateTimeOffset.TryParse(accessTokenExpirationText, out var accessTokenExpiration))
		{
			return;
		}

		var oidcOptions = oidcOptionsMonitor.Get(oidcScheme);
		var now = oidcOptions.TimeProvider!.GetUtcNow();
		if (now + TimeSpan.FromMinutes(5) < accessTokenExpiration)
		{
			return;
		}

		var oidcConfiguration = await oidcOptions.ConfigurationManager!.GetConfigurationAsync(validateContext.HttpContext.RequestAborted);
		var tokenEndpoint = oidcConfiguration.TokenEndpoint ?? throw new InvalidOperationException("Cannot refresh cookie. TokenEndpoint missing!");
		var refreshToken = validateContext.Properties.GetTokenValue("refresh_token") ?? throw new InvalidOperationException("Cannot refresh cookie. RefreshToken missing!");

		// When multiple requests come in parallel with expired access token
		// we want to use the refresh token only once to request a new access token.
		OpenIdConnectMessage message = null;
		await s_criticalSection.ExecuteActionAsync(refreshToken, async () =>
		{
			message = refreshTokenEndpointResponseCacheStore.GetResponse(refreshToken);
			if (message is null)
			{
				using var refreshResponse = await oidcOptions.Backchannel.PostAsync(tokenEndpoint,
					new FormUrlEncodedContent(new Dictionary<string, string>()
					{
						["grant_type"] = "refresh_token",
						["client_id"] = oidcOptions.ClientId,
						["client_secret"] = oidcOptions.ClientSecret,
						["scope"] = string.Join(" ", oidcOptions.Scope),
						["refresh_token"] = refreshToken
					}));

				if (!refreshResponse.IsSuccessStatusCode)
				{
					validateContext.RejectPrincipal();
					return;
				}

				var refreshJson = await refreshResponse.Content.ReadAsStringAsync();
				message = new OpenIdConnectMessage(refreshJson);
				refreshTokenEndpointResponseCacheStore.StoreResponse(refreshToken, message);
			}
		});

		if (message is null)
		{
			validateContext.RejectPrincipal();
			return;
		}

		var validationParameters = oidcOptions.TokenValidationParameters.Clone();
		if (oidcOptions.ConfigurationManager is BaseConfigurationManager baseConfigurationManager)
		{
			validationParameters.ConfigurationManager = baseConfigurationManager;
		}
		else
		{
			validationParameters.ValidIssuer = oidcConfiguration.Issuer;
			validationParameters.IssuerSigningKeys = oidcConfiguration.SigningKeys;
		}

		var validationResult = await oidcOptions.TokenHandler.ValidateTokenAsync(message.IdToken, validationParameters);

		if (!validationResult.IsValid)
		{
			validateContext.RejectPrincipal();
			return;
		}

		var validatedIdToken = JwtSecurityTokenConverter.Convert(validationResult.SecurityToken as JsonWebToken);
		validatedIdToken.Payload["nonce"] = null;
		oidcTokenValidator.ValidateTokenResponse(new()
		{
			ProtocolMessage = message,
			ClientId = oidcOptions.ClientId,
			ValidatedIdToken = validatedIdToken,
		});

		validateContext.ShouldRenew = true;
		validateContext.ReplacePrincipal(new ClaimsPrincipal(
		[
			new ClaimsIdentity(validationResult.ClaimsIdentity),
			// We will also add our custom claims that we created in the CustomClaimsBuilder.
			new ClaimsIdentity(
				validateContext.Principal.Claims.Where(claim => claim.Issuer == ClaimConstants.ApplicationIssuer),
				authenticationType: nameof(CustomClaimsBuilder),
				nameType: validationResult.ClaimsIdentity.NameClaimType,
				roleType: validationResult.ClaimsIdentity.RoleClaimType)
		]));

		var expiresIn = int.Parse(message.ExpiresIn, NumberStyles.Integer, CultureInfo.InvariantCulture);
		var expiresAt = now + TimeSpan.FromSeconds(expiresIn);
		validateContext.Properties.StoreTokens([
			new() { Name = "access_token", Value = message.AccessToken },
			new() { Name = "id_token", Value = message.IdToken },
			new() { Name = "refresh_token", Value = message.RefreshToken },
			new() { Name = "token_type", Value = message.TokenType },
			new() { Name = "expires_at", Value = expiresAt.ToString("o", CultureInfo.InvariantCulture) },
		]);
	}
}
