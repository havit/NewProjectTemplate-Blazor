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
		public static int GetUserId(this ClaimsPrincipal principal)
		{
			Claim userIdClaim = principal.Claims.Single(claim => (claim.Type == ClaimConstants.UserIdClaim) && (claim.Issuer == ClaimConstants.ApplicationIssuer));
			return Int32.Parse(userIdClaim.Value);
		}
	}
}
