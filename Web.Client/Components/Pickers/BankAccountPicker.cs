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
	public class BankAccountPicker : HxSelect<int?, BankAccountDto>
	{
		[Inject] protected IBankAccountDataStore BankAccountDataStore { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		protected override async Task OnInitializedAsync()
		{
			this.Nullable = true;
			this.NullText = GlobalLocalizer.SelectNull;
			this.NullDataText = GlobalLocalizer.SelectNullItems;
			this.Data = await BankAccountDataStore.GetAllAsync();
			this.ValueSelector = (c => c.Id);
			this.TextSelector = (c => c.Name);
		}
	}
}
