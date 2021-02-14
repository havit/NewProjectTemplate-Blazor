using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.NewProjectTemplate.Contracts.Crm;

namespace Havit.NewProjectTemplate.Web.Client.Services.DataStores
{
	public interface IContactReferenceDataStore : IDictionaryStaticDataStore<int, ContactReferenceVM>
	{
	}
}