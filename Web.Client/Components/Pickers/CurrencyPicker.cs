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
	public partial class CurrencyPicker : HxSelect<int?, CurrencyDto>
	{
		[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		public CurrencyPicker()
		{
			this.Nullable = true;
			this.ValueSelector = (c => c.Id);
			this.TextSelector = (c => c.Code);
		}

		protected override async Task OnInitializedAsync()
		{
			this.NullText ??= GlobalLocalizer.SelectNull;
			this.NullItemsText ??= GlobalLocalizer.SelectNullItems;

			this.Items = await CurrencyDataStore.GetAllAsync();
		}
	}
}
