using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.NewProjectTemplate.Contracts.Crm;
using Havit.NewProjectTemplate.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Components.Pickers
{
	public class ContactPicker : HxAutosuggest<ContactReferenceVM, int?>
	{
		[Inject] protected IContactReferenceDataStore ContactReferenceDataStore { get; set; }

		public ContactPicker()
		{
			this.DataProvider = GetSuggestions;
			this.ItemFromValueResolver = ResolveItemFromId;
			this.TextSelector = (c => c.Name);
			this.ValueSelector = (v => v.Id);
		}

		private async Task<AutosuggestDataProviderResult<ContactReferenceVM>> GetSuggestions(AutosuggestDataProviderRequest request)
		{
			var all = await ContactReferenceDataStore.GetAllAsync();

			// TODO StartsWith first, Contains then, see G2
			return new AutosuggestDataProviderResult<ContactReferenceVM>()
			{
				Data = all
							.Where(c => c.Name.StartsWith(request.UserInput, StringComparison.OrdinalIgnoreCase))
							.OrderBy(c => c.Name)
							.Take(10)
							.ToList()
			};
		}

		private async Task<ContactReferenceVM> ResolveItemFromId(int? id)
		{
			if (id is null)
			{
				return null;
			}
			return (await ContactReferenceDataStore.GetByKeyAsync(id.Value));
		}
	}
}
