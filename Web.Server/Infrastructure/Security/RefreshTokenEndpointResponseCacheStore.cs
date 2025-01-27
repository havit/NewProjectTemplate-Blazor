using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

public sealed class RefreshTokenEndpointResponseCacheStore(IMemoryCache memoryCache)
	: IRefreshTokenEndpointResponseCacheStore
{
	public OpenIdConnectMessage GetResponse(string refreshToken)
	{
		return memoryCache.TryGetValue(refreshToken, out OpenIdConnectMessage response)
			? response
			: null;
	}

	public void StoreResponse(string refreshToken, OpenIdConnectMessage response)
	{
		memoryCache.Set(refreshToken, response, new MemoryCacheEntryOptions
		{
			Priority = CacheItemPriority.Low,
			AbsoluteExpirationRelativeToNow = new TimeSpan(0, 15, 0) /* 15 minutes */
		});
	}
}