using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class ExchangeRateEdit
	{
		[Parameter] public ExchangeRateDto Value { get; set; }
		[Parameter] public EventCallback<ExchangeRateDto> ValueChanged { get; set; }
		[Parameter] public LayoutDisplayMode DisplayMode { get; set; } = LayoutDisplayMode.Drawer;
		[Parameter] public EventCallback OnClosed { get; set; }

		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IExchangeRateFacade ExchangeRateFacade { get; set; }
		[Inject] protected ICurrencyDataStore CurrencyDataStore { get; set; }
		[Inject] protected IExchangeRateLocalizer ExchangeRateLocalizer { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		private ExchangeRateDto model;
		private HxDisplayLayout hxDisplayLayout;
		private Dictionary<int, CurrencyDto> currencies;

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			model = this.Value with { }; // Clone!
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			currencies = (await CurrencyDataStore.GetAllAsync()).ToDictionary(c => c.Id);
		}

		public async Task HandleValidSubmit()
		{
			try
			{
				if (model.Id == default)
				{
					model.Id = (await ExchangeRateFacade.CreateExchangeRateAsync(model)).Value;
					Messenger.AddInformation(GetExchangeRateLabel(model), GlobalLocalizer.NewSuccess);
				}
				else
				{
					await ExchangeRateFacade.UpdateExchangeRateAsync(model);
					Messenger.AddInformation(GetExchangeRateLabel(model), GlobalLocalizer.UpdateSuccess);
				}

				await hxDisplayLayout.HideAsync();

				Value.UpdateFrom(model);
				await ValueChanged.InvokeAsync(this.Value);
			}
			catch (OperationFailedException)
			{
				// NOOP - The user should be able to fix the issues and repeat the action
			}
		}

		public Task ShowAsync() => hxDisplayLayout.ShowAsync();

		private string GetExchangeRateLabel(ExchangeRateDto exchangeRate)
		{
			var currency = currencies[exchangeRate.CurrencyId.Value];
			return $"{currency.Code} - {exchangeRate.DateFrom:g}";
		}
	}
}
