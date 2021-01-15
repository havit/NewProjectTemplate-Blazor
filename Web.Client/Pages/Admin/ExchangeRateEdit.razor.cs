using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class ExchangeRateEdit
	{
		[Parameter] public ExchangeRateDto Value { get; set; } = new ExchangeRateDto(); // TODO remove, testovací
		[Parameter] public EventCallback<ExchangeRateDto> ValueChanged { get; set; }
		[Parameter] public LayoutDisplayMode DisplayMode { get; set; } = LayoutDisplayMode.Plain;

		[Inject] protected IHxMessengerService Messenger { get; set; }
		//[Inject] protected IBankAccountFacade BankAccountFacade { get; set; }
		//[Inject] protected IExchangeRateLocalizer BankAccountLoc { get; set; }
		//[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLoc { get; set; }

		private ExchangeRateDto model = new ExchangeRateDto();
		private string title = String.Empty;
		private HxDisplayLayout hxDisplayLayout;

		protected async override Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();

			model = this.Value with { }; // Clone!
			//title = await GetTitleAsync();
		}

		//private async Task<string> GetTitleAsync()
		//{
		//	if (model.Id == default)
		//	{
		//		return "ExchnageRateLoc.New"; // TODO
		//	}
		//	var currencyCode = await CurrencyDataStore.GetByKeyAsync(model.CurrencyId);
		//	return $"{currencyCode} - {model.DateFrom}";
		//}

		//public async Task HandleValidSubmit()
		//{
		//	if (model.Id == default)
		//	{
		//		// TODO model.Id = (await BankAccountFacade.CreateBankAccountAsync(model)).Value;
		//		Messenger.AddInformation($"{await CurrencyDataStore.GetByKeyAsync(model.CurrencyId)} - {model.DateFrom}", GlobalLoc.NewSuccess);
		//	}
		//	else
		//	{
		//		// TODO await BankAccountFacade.UpdateBankAccountAsync(model);
		//		Messenger.AddInformation($"{await CurrencyDataStore.GetByKeyAsync(model.CurrencyId)} - {model.DateFrom}", GlobalLoc.UpdateSuccess);
		//	}

		//	Value.UpdateFrom(model);
		//	await ValueChanged.InvokeAsync(this.Value);

		//	await hxDisplayLayout.HideAsync();
		//}

		//public Task ShowAsync() => hxDisplayLayout.ShowAsync();
	}
}
