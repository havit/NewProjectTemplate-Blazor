using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	public interface ICurrencyFacade
	{
		Task<Dto<List<CurrencyDto>>> GetAllAsync(CancellationToken cancellationToken = default);
	}
}
