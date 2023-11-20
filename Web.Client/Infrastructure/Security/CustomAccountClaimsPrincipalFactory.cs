using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

// https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-identity-server?view=aspnetcore-5.0&tabs=visual-studio#custom-user-factory
public class CustomAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
	private readonly IUserClaimsRetrievalService _userClaimsRetrievalService;

	public CustomAccountClaimsPrincipalFactory(
		IAccessTokenProviderAccessor accessor,
		IUserClaimsRetrievalService userClaimsRetrievalService
		) : base(accessor)
	{
		_userClaimsRetrievalService = userClaimsRetrievalService;
	}

	public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
	{
		var user = await base.CreateUserAsync(account, options);

		if (user.Identity.IsAuthenticated)
		{
			var identity = (ClaimsIdentity)user.Identity;

			var claims = await _userClaimsRetrievalService.FetchAdditionalUserClaimsAsync(this.TokenProvider);
			if (claims != null)
			{
				identity.AddClaims(claims);
			}
			else
			{
				// null indicates rejection from the server
				return new ClaimsPrincipal();
			}
		}

		return user;
	}
}
