using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.DataLayer.Repositories.Finance;
using Havit.GoranG3.Services.Finance;

namespace Havit.GoranG3.Facades.Finance
{
	public class BankAccountFacade : IBankAccountFacade
	{
		private readonly IBankAccountRepository bankAccountRepository;
		private readonly IBankAccountMapper bankAccountMapper;
		private readonly IUnitOfWork unitOfWork;

		public BankAccountFacade(
			IBankAccountRepository bankAccountRepository,
			IBankAccountMapper bankAccountMapper,
			IUnitOfWork unitOfWork)
		{
			this.bankAccountRepository = bankAccountRepository;
			this.bankAccountMapper = bankAccountMapper;
			this.unitOfWork = unitOfWork;
		}

		public async Task<Dto<List<BankAccountDto>>> GetBankAccountsAsync()
		{
			var data = await bankAccountRepository.GetAllAsync();
			return Dto.FromValue(data.Select(ba => bankAccountMapper.MapToBankAccountDto(ba)).ToList());
		}

		public async Task DeleteBankAccountAsync(Dto<int> bankAccountId)
		{
			var bankAccount = await bankAccountRepository.GetObjectAsync(bankAccountId.Value);
			unitOfWork.AddForDelete(bankAccount);
			await unitOfWork.CommitAsync();
		}
	}
}
