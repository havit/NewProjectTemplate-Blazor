using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Finance;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public interface ICurrencyDataStore : IDictionaryStaticDataStore<int, CurrencyDto>
	{

	}
}
