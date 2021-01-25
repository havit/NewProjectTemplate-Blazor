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
	public class BankAccountMapper : IBankAccountMapper
	{
		public void MapFromBankAccountDto(BankAccountDto bankAccountDto, BankAccount bankAccount)
		{
			Contract.Requires<ArgumentNullException>(bankAccountDto is not null);
			Contract.Requires<ArgumentNullException>(bankAccount is not null);

			bankAccount.Name = bankAccountDto.Name;
			bankAccount.BankName = bankAccountDto.BankName;
			bankAccount.AccountNumber = bankAccountDto.AccountNumber;
			bankAccount.Iban = bankAccountDto.Iban;
			bankAccount.SwiftBic = bankAccountDto.SwiftBic;
		}

		public BankAccountDto MapToBankAccountDto(BankAccount bankAccount)
		{
			Contract.Requires<ArgumentNullException>(bankAccount is not null);

			return new BankAccountDto
			{
				Id = bankAccount.Id,
				Name = bankAccount.Name,
				BankName = bankAccount.BankName,
				AccountNumber = bankAccount.AccountNumber,
				Iban = bankAccount.Iban,
				SwiftBic = bankAccount.SwiftBic
			};
		}
	}
}
