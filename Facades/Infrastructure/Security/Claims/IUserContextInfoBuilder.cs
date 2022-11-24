using System.Security.Claims;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

public interface IUserContextInfoBuilder
{
	UserContextInfo GetUserContextInfo(ClaimsPrincipal principal);
}
