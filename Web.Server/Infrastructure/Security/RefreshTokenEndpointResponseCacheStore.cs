using Havit.Services.Caching;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

public sealed class RefreshTokenEndpointResponseCacheStore(ICacheService cacheService)
	: IRefreshTokenEndpointResponseCacheStore
{
	public OpenIdConnectMessage GetResponse(string refreshToken)
	{
		return cacheService.TryGet(refreshToken, out OpenIdConnectMessage response)
			? response
			: null;
	}

	public void StoreResponse(string refreshToken, OpenIdConnectMessage response)
	{
		cacheService.Add(refreshToken, response, new CacheOptions
		{
			Priority = CacheItemPriority.Low,
			AbsoluteExpiration = TimeSpan.FromMinutes(15)
		});
	}
}