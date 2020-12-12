using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	public interface IBankAccountFacade
	{
		Task<Dto<List<BankAccountDto>>> GetBankAccountsAsync();
		Task DeleteBankAccountAsync(Dto<int> bankAccountId);
		Task<Dto<int>> CreateBankAccountAsync(BankAccountDto bankAccountDto);
		Task UpdateBankAccountAsync(BankAccountDto bankAccountDto);
	}
}
