using System.Security.Claims;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

public interface IClaimsCacheStore
{
	List<Claim> GetClaims(UserContextInfo userContextInfo);

	void StoreClaims(UserContextInfo userContextInfo, List<Claim> claims);
}
