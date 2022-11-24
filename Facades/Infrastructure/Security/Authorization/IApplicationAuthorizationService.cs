using Havit.NewProjectTemplate.Primitives.Model.Security;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

/// <summary>
/// Autorizační služby pro aplikaci.
/// </summary>
public interface IApplicationAuthorizationService
{
	/// <summary>
	/// Vrátí seznam rolí aktuálního uživatele.
	/// </summary>
	/// <returns></returns>
	IEnumerable<RoleEntry> GetCurrentUserRoles();

	/// <summary>
	/// Vrací true, pokud má uživatel požadovanou roli.
	/// </summary>
	bool IsCurrentUserInRole(RoleEntry role);
}
