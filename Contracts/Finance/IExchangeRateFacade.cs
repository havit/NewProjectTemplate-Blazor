using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	[ServiceContract]
	public interface IExchangeRateFacade
	{
		Task<Dto<List<ExchangeRateDto>>> GetExchangeRatesAsync(CancellationToken cancellationToken = default);
		Task DeleteExchangeRateAsync(Dto<int> exchangeRateId, CancellationToken cancellationToken = default);
		Task<Dto<int>> CreateExchangeRateAsync(ExchangeRateDto exchangeRateDto, CancellationToken cancellationToken = default);
		Task UpdateExchangeRateAsync(ExchangeRateDto exchangeRateDto, CancellationToken cancellationToken = default);
	}
}
