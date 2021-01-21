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
	public interface ICurrencyFacade
	{
		Task<Dto<List<CurrencyDto>>> GetAllAsync(CancellationToken cancellationToken = default);
		Task DeleteCurrencyAsync(Dto<int> currencyId, CancellationToken cancellationToken = default);
		Task<Dto<int>> CreateCurrencyAsync(CurrencyDto currencyDto, CancellationToken cancellationToken = default);
		Task UpdateCurrencyAsync(CurrencyDto currencyDto, CancellationToken cancellationToken = default);
	}
}
