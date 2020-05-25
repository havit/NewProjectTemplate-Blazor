using System;
using System.Collections.Generic;
using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Services.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Claims
{
	/// <summary>
	/// (In-memory) cache claimů. 
	/// </summary>
	//[Service(Profile = ServiceProfiles.WebServer)]
	public class ClaimsCacheStore : IClaimsCacheStore
	{
		private readonly IMemoryCache cache;

		public ClaimsCacheStore(Microsoft.Extensions.Caching.Memory.IMemoryCache cache)
		{
			this.cache = cache;
		}

		public List<Claim> GetClaims(UserContextInfo userContextInfo)
		{
			if (cache.TryGetValue(GetCacheKey(userContextInfo), out List<Claim> result))
			{
				return result;
			}
			return null;
		}

		public void StoreClaims(UserContextInfo userContextInfo, List<Claim> claims)
		{
			cache.Set(GetCacheKey(userContextInfo), claims, new MemoryCacheEntryOptions
			{
				Priority = CacheItemPriority.Low,
				AbsoluteExpirationRelativeToNow = new TimeSpan(0, 15, 0) /* 15 minut */
			});
		}

		private object GetCacheKey(UserContextInfo userContextInfo)
		{
			// implementace má přetížen GetHashCode i Equals
			return userContextInfo;
		}
	}
}
