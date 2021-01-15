using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Components.Web.Forms;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Components.Pickers
{
	public partial class CurrencyPicker : HxSelect<int?, CurrencyDto>
	{
		[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }

		protected async override Task OnInitializedAsync()
		{
			this.Nullable = true;
			this.Items = new List<CurrencyDto>() { new() { Id = 1, Code = "Kč" }, new() { Id = 2, Code = "EUR" } }; // await CurrencyDataStore.GetAllAsync();
			this.ValueSelector = (c => c.Id);
			this.TextSelector = (c => c.Code);
		}
	}
}
