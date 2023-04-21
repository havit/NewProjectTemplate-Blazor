using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

// https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-identity-server?view=aspnetcore-5.0&tabs=visual-studio#custom-user-factory
public class CustomAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
	private readonly IUserClaimsRetrievalService userClaimsRetrievalService;

	public CustomAccountClaimsPrincipalFactory(
		IAccessTokenProviderAccessor accessor,
		IUserClaimsRetrievalService userClaimsRetrievalService
		) : base(accessor)
	{
		this.userClaimsRetrievalService = userClaimsRetrievalService;
	}

	public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
	{
		var user = await base.CreateUserAsync(account, options);

		if (user.Identity.IsAuthenticated)
		{
			var identity = (ClaimsIdentity)user.Identity;

			var claims = await userClaimsRetrievalService.FetchAdditionalUserClaimsAsync(this.TokenProvider);

			foreach (var claim in claims)
			{
				if (claim.Type.Equals(ClaimTypes.Role))
				{
					identity.AddClaim(new Claim(options.RoleClaim, claim.Value));
				}
				else
				{
					identity.AddClaim(claim);
				}
			}
		}

		return user;
	}
}
