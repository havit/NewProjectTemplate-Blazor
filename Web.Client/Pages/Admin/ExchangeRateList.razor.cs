using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class ExchangeRateList
	{
		[Inject] protected IExchangeRateFacade ExchangeRateFacade { get; set; }
		[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }
		[Inject] protected IExchangeRateLocalizer ExchangeRateLocalizer { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }
		[Inject] protected IHxMessengerService Messenger { get; set; }

		private ExchangeRateDto exchangeRateSelected;
		private ExchangeRateDto exchangeRateEdited = new ExchangeRateDto();
		private HxGrid<ExchangeRateDto> exchangeRatesGrid;
		private ExchangeRateEdit exchangeRateEditComponent;
		private Dictionary<int, CurrencyDto> currencies;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await EnsureCurrencies();
		}

		private async Task EnsureCurrencies()
		{
			if (currencies == null)
			{
				currencies = (await CurrencyDataStore.GetAllAsync()).ToDictionary(c => c.Id);
			}
		}

		private async Task<GridDataProviderResult<ExchangeRateDto>> LoadExchangeRates(GridDataProviderRequest<ExchangeRateDto> request)
		{
			await EnsureCurrencies();
			return request.ApplyTo((await ExchangeRateFacade.GetExchangeRatesAsync()).Value);
		}

		private async Task HandleSelectedDataItemChanged(ExchangeRateDto selection)
		{
			exchangeRateSelected = selection;
			exchangeRateEdited = selection;
			await exchangeRateEditComponent.ShowAsync();
		}

		private async Task HandleNewItemClicked()
		{
			exchangeRateEdited = new ExchangeRateDto();
			await exchangeRateEditComponent.ShowAsync();
		}

		private async Task HandleDeleteItemClicked(ExchangeRateDto exchangeRate)
		{
			await ExchangeRateFacade.DeleteExchangeRateAsync(Dto.FromValue(exchangeRate.Id));
			Messenger.AddInformation(GetExchangeRateLabel(exchangeRate), GlobalLocalizer.DeleteSuccess);
			await exchangeRatesGrid.RefreshDataAsync();
		}

		private async Task HandleEditValueChanged()
		{
			await exchangeRatesGrid.RefreshDataAsync();
		}

		private void HandleEditClosed()
		{
			exchangeRateSelected = null;
			exchangeRateEdited = new ExchangeRateDto();
		}

		private string GetExchangeRateLabel(ExchangeRateDto exchangeRate)
		{
			var currency = currencies[exchangeRate.CurrencyId.Value];
			return $"{currency.Code} - {exchangeRate.DateFrom:g}";
		}
	}
}
