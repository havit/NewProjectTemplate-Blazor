using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Finance;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public class CurrencyDataStore : DictionaryStaticDataStore<int, CurrencyDto>, ICurrencyDataStore
	{
		private readonly ICurrencyFacade currencyFacade;

		public CurrencyDataStore(ICurrencyFacade currencyFacade)
		{
			this.currencyFacade = currencyFacade;
		}

		protected override Func<CurrencyDto, int> KeySelector => (currency) => currency.Id;
		protected override bool ShouldRefresh() => false; // just hit F5 :-D

		protected async override Task<IEnumerable<CurrencyDto>> LoadDataAsync()
		{
			var dto = await currencyFacade.GetAllAsync();
			return dto.Value;
		}
	}
}
