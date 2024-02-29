using System.Security.Claims;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

public interface ICustomClaimsBuilder
{
	Task<List<Claim>> GetCustomClaimsAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default);
}
