using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Diagnostics.Contracts;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Services.Finance
{
	[Service(Lifetime = ServiceLifetime.Singleton)]
	public class ExchangeRateMapper : IExchangeRateMapper
	{
		public void MapFromExchangeRateDto(ExchangeRateDto exchangeRateDto, ExchangeRate exchangeRate)
		{
			Contract.Requires<ArgumentNullException>(exchangeRateDto is not null);
			Contract.Requires<ArgumentNullException>(exchangeRate is not null);

			exchangeRate.Id = exchangeRateDto.Id;
			exchangeRate.CurrencyId = exchangeRateDto.CurrencyId ?? throw new ArgumentException(nameof(exchangeRateDto), "The value of 'exchangeRateDto.CurrencyId' should not be null");
			exchangeRate.DateFrom = exchangeRateDto.DateFrom ?? throw new ArgumentException(nameof(exchangeRateDto), "The value of 'exchangeRateDto.DateFrom' should not be null");
			exchangeRate.Rate = exchangeRateDto.Rate ?? throw new ArgumentException(nameof(exchangeRateDto), "The value of 'exchangeRateDto.Rate' should not be null");
		}

		public ExchangeRateDto MapToExchangeRateDto(ExchangeRate exchangeRate)
		{
			Contract.Requires<ArgumentNullException>(exchangeRate is not null);

			return new ExchangeRateDto
			{
				Id = exchangeRate.Id,
				CurrencyId = exchangeRate.CurrencyId,
				DateFrom = exchangeRate.DateFrom,
				Rate = exchangeRate.Rate
			};
		}
	}
}
