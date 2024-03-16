using Havit.NewProjectTemplate.Primitives.Security;

namespace Havit.NewProjectTemplate.Services.Infrastructure.Security;

public interface IApplicationAuthorizationService
{
	IEnumerable<RoleEntry> GetCurrentUserRoles();

	bool IsCurrentUserInRole(RoleEntry role);
}
