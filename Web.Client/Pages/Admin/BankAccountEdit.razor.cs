using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Components.Web.Bootstrap.Layouts;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	// TODO: Detekce změn + onBeforeUnload? nebo alespoň před přepnutím na jinou instanci?
	public partial class BankAccountEdit : ComponentBase
	{
		[Parameter] public BankAccountDto Value { get; set; }
		[Parameter] public EventCallback<BankAccountDto> ValueChanged { get; set; }

		[CascadingParameter] protected LayoutDetailContext LayoutDetailContext { get; set; }

		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] protected IBankAccountLocalizer BankAccountLoc { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLoc { get; set; }
		[Inject] protected ILogger<BankAccountEdit> Logger { get; set; }

		private BankAccountDto model;

		protected override void OnParametersSet()
		{
			Logger.LogDebug("OnParametersSet");

			base.OnParametersSet();

			model = this.Value with { }; // Clone!

			if (LayoutDetailContext is not null)
			{
				// propagation of the Title to parent container (Drawer, Dialog, ...)
				LayoutDetailContext.DetailTitle = model.Name;
			}
		}

		public async Task HandleValidSubmit()
		{
			if (model.Id == default)
			{
				model.Id = (await BankAccountFacade.CreateBankAccountAsync(model)).Value;
				Messenger.AddInformation(model.Name, GlobalLoc.NewSuccess);
			}
			else
			{
				await BankAccountFacade.UpdateBankAccountAsync(model);
				Messenger.AddInformation(model.Name, GlobalLoc.UpdateSuccess);
			}

			Value.UpdateFrom(model);
			await ValueChanged.InvokeAsync(this.Value);
		}
	}
}
