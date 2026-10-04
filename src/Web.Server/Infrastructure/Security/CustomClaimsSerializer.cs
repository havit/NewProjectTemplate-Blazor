using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

public static class CustomClaimsSerializer
{
	/// <summary>
	/// Serializes custom set of claims.
	/// </summary>
	public static ValueTask<AuthenticationStateData> SerializeAuthenticationStateAsync(AuthenticationState authenticationState)
	{
		if (authenticationState.User.Identity?.IsAuthenticated ?? false)
		{
			ClaimsIdentity firstIdentity = authenticationState.User.Identities.First();

			var claimData = authenticationState.User.Claims
				.Where(claim => (claim.Type == firstIdentity.NameClaimType)
					|| (claim.Type == firstIdentity.RoleClaimType))
				.Select(claim => new ClaimData(claim))
				.ToArray();

			return ValueTask.FromResult(new AuthenticationStateData
			{
				Claims = claimData,
				NameClaimType = firstIdentity.NameClaimType,
				RoleClaimType = firstIdentity.RoleClaimType
			});
		}

		return ValueTask.FromResult<AuthenticationStateData>(null);
	}
}
