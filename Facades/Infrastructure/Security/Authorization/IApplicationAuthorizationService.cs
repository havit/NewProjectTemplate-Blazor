using Havit.NewProjectTemplate.Primitives.Security;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

public interface IApplicationAuthorizationService
{
	IEnumerable<RoleEntry> GetCurrentUserRoles();

	bool IsCurrentUserInRole(RoleEntry role);
}
