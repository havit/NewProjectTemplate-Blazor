using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Crm;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public interface IContactReferenceDataStore : IDictionaryStaticDataStore<int, ContactReferenceVM>
	{
	}
}