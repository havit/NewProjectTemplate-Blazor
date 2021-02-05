using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Services.DataStores;
using Havit.GoranG3.Contracts.Crm;

namespace Havit.GoranG3.Web.Client.Services.DataStores
{
	public class ContactReferenceDataStore : DictionaryStaticDataStore<int, ContactReferenceVM>, IContactReferenceDataStore
	{
		private readonly IContactFacade contactFacade;

		public ContactReferenceDataStore(IContactFacade contactFacade)
		{
			this.contactFacade = contactFacade;
		}

		protected override Func<ContactReferenceVM, int> KeySelector => (contact) => contact.Id;
		protected override bool ShouldRefresh() => false; // just hit F5 :-D

		protected async override Task<IEnumerable<ContactReferenceVM>> LoadDataAsync()
		{
			var dto = await contactFacade.GetAllContactReferencesAsync();
			return dto.Value;
		}
	}
}
