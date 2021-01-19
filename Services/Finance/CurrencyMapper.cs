using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Services.Finance
{
	[Service(Lifetime = ServiceLifetime.Singleton)]
	public class CurrencyMapper : ICurrencyMapper
	{
		public void MapFromCurrencyDto(CurrencyDto currencyDto, Currency currency)
		{
			currency.DefaultBankAccountId = currencyDto.DefaultBankAccountId;
			currency.Code = currencyDto.Code;
		}

		public CurrencyDto MapToCurrencyDto(Currency currency)
		{
			return new CurrencyDto
			{
				Id = currency.Id,
				DefaultBankAccountId = currency.DefaultBankAccountId,
				Code = currency.Code
			};
		}
	}
}
