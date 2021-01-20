using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class CurrencyList : ComponentBase
	{
		[Inject] protected ICurrencyFacade CurrencyFacade { get; set; }
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected ICurrencyLocalizer CurrencyLocalizer { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		private List<CurrencyDto> currencies;
		private CurrencyDto editedCurrency = new CurrencyDto();
		private CurrencyDto selectedCurrency;
		private CurrencyEdit currencyEditComponent;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			currencies = (await CurrencyFacade.GetAllAsync()).Value;
		}

		private async Task DeleteItemClicked(CurrencyDto currencyDto)
		{
			await CurrencyFacade.DeleteCurrencyAsync(Dto.FromValue(currencyDto.Id));
			Messenger.AddInformation(currencyDto.Code, GlobalLocalizer.DeleteSuccess);
			await LoadDataAsync();
		}

		private async Task HandleSelectedDataItemChanged(CurrencyDto selection)
		{
			editedCurrency = selection;
			await currencyEditComponent.ShowAsync();
		}

		private async Task NewItemClicked()
		{
			editedCurrency = new CurrencyDto();
			await currencyEditComponent.ShowAsync();
		}

		private async Task HandleEditValueChanged()
		{
			await LoadDataAsync();
		}

		private void HandleEditClosed()
		{
			selectedCurrency = null;
			editedCurrency = new CurrencyDto();
		}
	}
}
