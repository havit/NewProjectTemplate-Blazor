using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.DataLayer.Repositories.Finance;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Havit.GoranG3.Model.Finance;
using Havit.GoranG3.Services.Finance;

namespace Havit.GoranG3.Facades.Finance
{
	public class BankAccountFacade : IBankAccountFacade
	{
		private readonly IBankAccountRepository bankAccountRepository;
		private readonly IBankAccountMapper bankAccountMapper;
		private readonly IApplicationAuthenticationService applicationAuthenticationService;
		private readonly IUnitOfWork unitOfWork;

		public BankAccountFacade(
			IBankAccountRepository bankAccountRepository,
			IBankAccountMapper bankAccountMapper,
			IApplicationAuthenticationService applicationAuthenticationService, // TODO XyAuthorizationService
			IUnitOfWork unitOfWork)
		{
			this.bankAccountRepository = bankAccountRepository;
			this.bankAccountMapper = bankAccountMapper;
			this.applicationAuthenticationService = applicationAuthenticationService;
			this.unitOfWork = unitOfWork;
		}

		public async Task<Dto<List<BankAccountDto>>> GetBankAccountsAsync(CancellationToken cancellationToken = default)
		{
			var data = await bankAccountRepository.GetAllAsync();
			return Dto.FromValue(data.Select(ba => bankAccountMapper.MapToBankAccountDto(ba)).ToList());
		}

		public async Task DeleteBankAccountAsync(Dto<int> bankAccountId, CancellationToken cancellationToken = default)
		{
			CheckAuthorization();

			var bankAccount = await bankAccountRepository.GetObjectAsync(bankAccountId.Value);
			unitOfWork.AddForDelete(bankAccount);
			await unitOfWork.CommitAsync();
		}

		public async Task<Dto<int>> CreateBankAccountAsync(BankAccountDto bankAccountDto, CancellationToken cancellationToken = default)
		{
			CheckAuthorization();

			var bankAccount = new BankAccount();
			bankAccountMapper.MapFromBankAccountDto(bankAccountDto, bankAccount);

			unitOfWork.AddForInsert(bankAccount);
			await unitOfWork.CommitAsync();

			return Dto.FromValue(bankAccount.Id);
		}

		public async Task UpdateBankAccountAsync(BankAccountDto bankAccountDto, CancellationToken cancellationToken = default)
		{
			CheckAuthorization();

			var bankAccount = await bankAccountRepository.GetObjectAsync(bankAccountDto.Id);

			bankAccountMapper.MapFromBankAccountDto(bankAccountDto, bankAccount);

			unitOfWork.AddForUpdate(bankAccount);
			await unitOfWork.CommitAsync();
		}

		private void CheckAuthorization()
		{
			var user = applicationAuthenticationService.GetCurrentUser();

			if ((user is null) || !user.IsInRole(roleEntry: Model.Security.Role.Entry.UserSettingsAdministrator))
			{
				throw new SecurityException("Access denied.");
			}
		}
	}
}
