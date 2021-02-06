using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;
using Havit.GoranG3.Web.Client.Resources;

namespace Havit.GoranG3.Web.Client.Components.Pickers
{
	public class BankAccountPicker : HxSelectBase<int?, BankAccountDto>
	{
		[Inject] protected IBankAccountDataStore BankAccountDataStore { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		public BankAccountPicker()
		{
			this.NullableImpl = true;
			this.ValueSelectorImpl = (c => c.Id);
			this.TextSelectorImpl = (c => c.Name);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			this.NullTextImpl ??= GlobalLocalizer.SelectNull;
			this.NullDataTextImpl ??= GlobalLocalizer.SelectNullItems;

			this.DataImpl = await BankAccountDataStore.GetAllAsync();
		}
	}
}
