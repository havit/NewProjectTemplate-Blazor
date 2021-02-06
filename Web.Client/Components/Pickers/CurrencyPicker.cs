using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Components.Pickers
{
	public class CurrencyPicker : HxSelectBase<int?, CurrencyDto>
	{
		[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		public CurrencyPicker()
		{
			this.NullableImpl = true;
			this.ValueSelectorImpl = (c => c.Id);
			this.TextSelectorImpl = (c => c.Code);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			this.NullTextImpl ??= GlobalLocalizer.SelectNull;
			this.NullDataTextImpl ??= GlobalLocalizer.SelectNullItems;

			this.DataImpl = await CurrencyDataStore.GetAllAsync();
		}
	}
}
