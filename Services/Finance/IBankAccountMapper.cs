using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.Services.Finance
{
	public interface IBankAccountMapper
	{
		BankAccountDto MapToBankAccountDto(BankAccount bankAccount);
		void MapFromBankAccountDto(BankAccountDto bankAccountDto, BankAccount bankAccount);
	}
}