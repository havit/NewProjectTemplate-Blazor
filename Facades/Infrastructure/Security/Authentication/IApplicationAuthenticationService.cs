using System.Security.Claims;
using Havit.NewProjectTemplate.Model.Security;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;

public interface IApplicationAuthenticationService
{
	ClaimsPrincipal GetCurrentClaimsPrincipal();
	User GetCurrentUser();
}
