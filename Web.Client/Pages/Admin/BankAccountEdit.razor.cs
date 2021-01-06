using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	// TODO: Detekce změn + onBeforeUnload? nebo alespoň před přepnutím na jinou instanci?
	public partial class BankAccountEdit : ComponentBase
	{
		[Parameter] public BankAccountDto Value { get; set; }
		[Parameter] public EventCallback<BankAccountDto> ValueChanged { get; set; }

		[Inject] protected IMessenger Messenger { get; set; }
		[Inject] protected IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] protected IBankAccountLocalizer BankAccountLoc { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLoc { get; set; }

		private BankAccountDto model;

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			model = this.Value with { }; // Clone!
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
