using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Common;
using Havit.Services.Caching;
using Havit.Services.TimeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Services.TimeServices
{
	[Service]
	public class DateInfoProvider : IDateInfoProvider
	{
		private const string CacheKey = "DateInfoDtoDictionary";

		private readonly IDateInfoRepository dateInfoRepository;
		private readonly ICacheService cacheService;

		public DateInfoProvider(
			IDateInfoRepository dateInfoRepository,
			ICacheService cacheService)
		{
			this.dateInfoRepository = dateInfoRepository;
			this.cacheService = cacheService;
		}

		public IDateInfo GetDateInfo(DateTime date)
		{
			var dictionary = GetDictionary();
			dictionary.TryGetValue(date, out var result);
			return result;
		}

		private Dictionary<DateTime, DateInfoDto> GetDictionary()
		{
			if (!cacheService.TryGet(CacheKey, out Dictionary<DateTime, DateInfoDto> dictionary))
			{
				lock (cacheLock)
				{
					if (!cacheService.TryGet(CacheKey, out dictionary))
					{
						dictionary = dateInfoRepository.GetAll().Select(di => new DateInfoDto(di.Date, di.IsHoliday)).ToDictionary(di => di.Date);
						cacheService.Add(CacheKey, dictionary);
					}
				}
			}
			return dictionary;
		}
		private static object cacheLock = new object();

		internal class DateInfoDto : IDateInfo
		{
			public DateInfoDto(DateTime date, bool isHoliday)
			{
				Date = date;
				IsHoliday = isHoliday;
			}

			public DateTime Date { get; }
			public bool IsHoliday { get; }
		}
	}
}
