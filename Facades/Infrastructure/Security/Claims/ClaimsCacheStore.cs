using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Services.Caching;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

[Service(Profile = ServiceProfiles.WebServer)]
public class ClaimsCacheStore : IClaimsCacheStore
{
	private readonly ICacheService cacheService;

	public ClaimsCacheStore(ICacheService cacheService)
	{
		this.cacheService = cacheService;
	}

	public List<Claim> GetClaims(UserContextInfo userContextInfo)
	{
		if (cacheService.TryGet<List<Claim>>(GetCacheKey(userContextInfo), out List<Claim> claims))
		{
			return claims;
		}
		return null;
	}

	public void StoreClaims(UserContextInfo userContextInfo, List<Claim> claims)
	{
		cacheService.Add(GetCacheKey(userContextInfo), claims, new CacheOptions
		{
			Priority = CacheItemPriority.Low,
			AbsoluteExpiration = new TimeSpan(hours: 0, minutes: 15, seconds: 0)
		});
	}

	private string GetCacheKey(UserContextInfo userContextInfo)
	{
		return userContextInfo.ToString();
	}
}
