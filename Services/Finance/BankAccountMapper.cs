using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Model.Finance;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Services.Finance
{
	[Service(Lifetime = ServiceLifetime.Singleton)]
	public class BankAccountMapper : IBankAccountMapper
	{
		public BankAccountDto MapToBankAccountDto(BankAccount bankAccount)
		{
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
