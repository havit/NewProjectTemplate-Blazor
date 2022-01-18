using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

/// <summary>
/// Autorizační služby pro aplikaci.
/// </summary>
public interface IApplicationAuthorizationService
{
	/// <summary>
	/// Vrací true, pokud má uživatel dané oprávnění k danému resource.
	/// </summary>
	Task<bool> IsAuthorizedAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null);

	/// <summary>
	/// Pokud uživatel NEMÁ dané oprávnění k danému resource, vyhazuje SecurityException.
	/// </summary>
	Task VerifyAuthorizationAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null);

	/// <summary>
	/// Vrací true, pokud má aktuální uživatel dané oprávnění k danému resource.
	/// </summary>
	Task<bool> IsCurrentUserAuthorizedAsync(IAuthorizationRequirement requirement, object resource = null);

	/// <summary>
	/// Pokud aktuální uživatel NEMÁ dané oprávnění k danému resource, vyhazuje SecurityException.
	/// </summary>
	Task VerifyCurrentUserAuthorizationAsync(IAuthorizationRequirement requirement, object resource = null);
}
