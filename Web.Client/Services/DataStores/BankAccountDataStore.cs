using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Finance;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public class BankAccountDataStore : DictionaryStaticDataStore<int, BankAccountDto>, IBankAccountDataStore
	{
		private readonly IBankAccountFacade bankAccountFacade;

		public BankAccountDataStore(IBankAccountFacade bankAccountFacade)
		{
			this.bankAccountFacade = bankAccountFacade;
		}

		protected override Func<BankAccountDto, int> KeySelector => bankAccount => bankAccount.Id;

		protected async override Task<IEnumerable<BankAccountDto>> LoadDataAsync()
		{
			var dto = await bankAccountFacade.GetBankAccountsAsync();
			return dto.Value;
		}

		protected override bool ShouldRefresh() => false;
	}
}
