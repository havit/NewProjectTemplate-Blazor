using System.Security.Claims;
using Havit.NewProjectTemplate.Contracts.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Havit.NewProjectTemplate.Web.Server.Controllers;

[Authorize]
public class UserProfileController : ControllerBase
{
	[HttpGet("/user-claims")]
	public IEnumerable<KeyValuePair<string, string>> GetAdditionalClaims()
	{
		return HttpContext.User
			.FindAll(
				c => c.Type.Equals(ClaimTypes.Role)
				|| c.Type.Equals(ClaimConstants.UserIdClaim))
			.Select(c => new KeyValuePair<string, string>(c.Type, c.Value ?? String.Empty));
	}
}
