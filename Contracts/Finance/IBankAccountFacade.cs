using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	public interface IBankAccountFacade
	{
		Task<Dto<List<BankAccountDto>>> GetBankAccountsAsync(CancellationToken cancellationToken = default);
		Task DeleteBankAccountAsync(Dto<int> bankAccountId, CancellationToken cancellationToken = default);
		Task<Dto<int>> CreateBankAccountAsync(BankAccountDto bankAccountDto, CancellationToken cancellationToken = default);
		Task UpdateBankAccountAsync(BankAccountDto bankAccountDto, CancellationToken cancellationToken = default);
	}
}
