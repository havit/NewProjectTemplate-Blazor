using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

/// <summary>
/// Retrieves additional user claims from Web.Server for RolesAccountClaimsPrincipalFactory.
/// </summary>
/// <remarks>
/// We cannot use facade (gRPC stack) as it is dependent on full user context
/// UserClientService uses Web.Server named HttpClient via IHttpClientFactory to break the dependency cycle
/// https://github.com/dotnet/aspnetcore/issues/33787
/// https://stackoverflow.com/questions/70935768/call-api-from-accountclaimsprincipalfactory-in-blazor-wasm
/// </remarks>
public class UserClaimsRetrievalService : IUserClaimsRetrievalService
{
	private readonly IHttpClientFactory httpClientFactory;
	private readonly NavigationManager navigationManager;

	public UserClaimsRetrievalService(
		IHttpClientFactory httpClientFactory,
		NavigationManager navigationManager)
	{
		this.httpClientFactory = httpClientFactory;
		this.navigationManager = navigationManager;
	}

	public async Task<IEnumerable<Claim>> FetchAdditionalUserClaimsAsync(IAccessTokenProvider accessTokenProvider, CancellationToken cancellationToken = default)
	{
		using var httpClient = httpClientFactory.CreateClient("Web.Server");

		try
		{
			var claims = await httpClient.GetFromJsonAsync<List<KeyValuePair<string, string>>>("/user-claims", cancellationToken);
			return claims.Select(c => new Claim(c.Key, c.Value ?? String.Empty));
		}
		catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
		{
			navigationManager.NavigateTo(Routes.Errors.AccessDenied);
		}
		catch (AccessTokenNotAvailableException ex)
		{
			ex.Redirect();
		}

		throw new InvalidOperationException("Unreachable code.");
	}
}