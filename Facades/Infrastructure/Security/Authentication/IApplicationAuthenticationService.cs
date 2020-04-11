using System.Security.Claims;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Authentication
{
    /// <summary>
    /// Vrací aktuálně přihlášeného uživatele jako ClaimsPrincipal nebo LoginAccount.
    /// Implementace interface ve WebAPI.
    /// </summary>
    public interface IApplicationAuthenticationService
    {
        ClaimsPrincipal GetCurrentClaimsPrincipal();

		// TODO: Doplnit modelový objekt, pokud je potřeba
        //LoginAccount GetCurrentLoginAccount();
    }
}
