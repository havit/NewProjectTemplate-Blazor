using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Claims
{
	public static class ClaimsPrincipalExtensions
	{
		public static int GetLoginAccountId(this ClaimsPrincipal principal)
		{
			Claim loginAccountIdClaim = principal.Claims.Single(claim => (claim.Type == ClaimConstants.LoginAccountIdClaim) && (claim.Issuer == ClaimConstants.ApplicationIssuer));
			return Int32.Parse(loginAccountIdClaim.Value);
		}
	}
}
