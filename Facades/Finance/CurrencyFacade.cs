using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;

namespace Havit.GoranG3.Facades.Finance
{
	public class CurrencyFacade : ICurrencyFacade
	{
		public Task<Dto<List<CurrencyDto>>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(Dto.FromValue(new List<CurrencyDto>() { new() { Id = 1, Code = "Kč" }, new() { Id = 2, Code = "EUR" } })); // TODO RH Implementovat - mapper, ...
		}
	}
}
