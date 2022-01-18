using System.Security.Claims;
using Havit.NewProjectTemplate.Model.Security;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;

/// <summary>
/// Vrací aktuálně přihlášeného uživatele jako ClaimsPrincipal nebo LoginAccount.
/// Implementace interface ve Web.Server.
/// </summary>
public interface IApplicationAuthenticationService
{
	ClaimsPrincipal GetCurrentClaimsPrincipal();
	User GetCurrentUser();
}
