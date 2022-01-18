using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

// https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-identity-server?view=aspnetcore-5.0&tabs=visual-studio#custom-user-factory
public class RolesAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
	public RolesAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
	{
		// NOOP
	}

	public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
	{
		var user = await base.CreateUserAsync(account, options);

		if (user.Identity.IsAuthenticated)
		{
			var identity = (ClaimsIdentity)user.Identity;
			var roleClaims = identity.FindAll(identity.RoleClaimType).ToArray();

			if (roleClaims != null && roleClaims.Any())
			{
				foreach (var existingClaim in roleClaims)
				{
					identity.RemoveClaim(existingClaim);
				}

				var rolesElem = account.AdditionalProperties[identity.RoleClaimType];

				if (rolesElem is JsonElement roles)
				{
					if (roles.ValueKind == JsonValueKind.Array)
					{
						foreach (var role in roles.EnumerateArray())
						{
							identity.AddClaim(new Claim(options.RoleClaim, role.GetString()));
						}
					}
					else
					{
						identity.AddClaim(new Claim(options.RoleClaim, roles.GetString()));
					}
				}
			}
		}

		return user;
	}
}
