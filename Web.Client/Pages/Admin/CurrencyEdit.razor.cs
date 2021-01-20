using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class CurrencyEdit : ComponentBase
	{
		[Parameter] public CurrencyDto Value { get; set; }
		[Parameter] public EventCallback<CurrencyDto> ValueChanged { get; set; }
		[Parameter] public LayoutDisplayMode DisplayMode { get; set; } = LayoutDisplayMode.Drawer;
		[Parameter] public EventCallback OnClosed { get; set; }

		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected ICurrencyFacade CurrencyFacade { get; set; }
		[Inject] protected ICurrencyLocalizer CurrencyLocalizer { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		private CurrencyDto model;
		private HxDisplayLayout hxDisplayLayout;

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			model = this.Value with { }; // Clone!
		}

		public async Task HandleValidSubmit()
		{
			if (model.Id == default)
			{
				model.Id = (await CurrencyFacade.CreateCurrencyAsync(model)).Value;
				Messenger.AddInformation(model.Code, GlobalLocalizer.NewSuccess);
			}
			else
			{
				await CurrencyFacade.UpdateCurrencyAsync(model);
				Messenger.AddInformation(model.Code, GlobalLocalizer.UpdateSuccess);
			}

			await hxDisplayLayout.HideAsync();

			Value.UpdateFrom(model);
			await ValueChanged.InvokeAsync(this.Value);
		}


		public Task ShowAsync() => hxDisplayLayout.ShowAsync();
	}
}
