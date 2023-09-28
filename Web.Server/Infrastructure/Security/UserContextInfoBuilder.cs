using System.Security.Claims;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

public class UserContextInfoBuilder : IUserContextInfoBuilder
{
	private UserContextInfo _userContextInfo;

	public UserContextInfo GetUserContextInfo(ClaimsPrincipal principal)
	{
		// Do not use httpContextAccessor.HttpContext.User.Identity.IsAuthenticated as it is false in this moment
		if (!principal.Identity.IsAuthenticated)
		{
			return null;
		}

		if (_userContextInfo == null)
		{
			Claim externalIdClaim = principal.Claims.Single(claim => claim.Type == "oid");

			_userContextInfo = new UserContextInfo(IdentityProviderExternalId: externalIdClaim.Value);
		}

		return _userContextInfo;
	}
}
