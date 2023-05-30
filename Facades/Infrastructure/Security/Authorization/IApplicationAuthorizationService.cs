using Havit.NewProjectTemplate.Primitives.Model.Security;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

public interface IApplicationAuthorizationService
{
	IEnumerable<RoleEntry> GetCurrentUserRoles();

	bool IsCurrentUserInRole(RoleEntry role);
}
