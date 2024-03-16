using System.Security.Claims;
using Havit.NewProjectTemplate.Model.Security;

namespace Havit.NewProjectTemplate.Services.Infrastructure.Security;

public interface IApplicationAuthenticationService
{
	ClaimsPrincipal GetCurrentClaimsPrincipal();
	User GetCurrentUser();
}
