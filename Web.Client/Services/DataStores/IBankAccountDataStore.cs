using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Finance;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public interface IBankAccountDataStore : IDictionaryStaticDataStore<int, BankAccountDto>
	{
	}
}