using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Infrastructure.Security
{
    public class RolesAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
	{
		public RolesAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
		{
			// NOOP
		}

		public override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
		{
			var roles = account.AdditionalProperties["role"] as JsonElement?;
			if (roles?.ValueKind == JsonValueKind.Array)
			{
				account.AdditionalProperties.Remove("role");
				var claimsPrincipal = base.CreateUserAsync(account, options).Result;

				foreach (JsonElement element in roles.Value.EnumerateArray())
				{
					((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(new Claim("role", element.GetString()));
				}
				return new ValueTask<ClaimsPrincipal>(claimsPrincipal);
			}

			return base.CreateUserAsync(account, options);
		}
	}
}
