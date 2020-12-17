using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
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

		[Inject] public IMessenger Messenger { get; set; }
		[Inject] public IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] public IBankAccountLocalizer BankAccountLoc { get; set; }

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
				Messenger.AddInformation(String.Format(BankAccountLoc.NewSuccess, model.Name));
			}
			else
			{
				await BankAccountFacade.UpdateBankAccountAsync(model);
				Messenger.AddInformation(String.Format(BankAccountLoc.UpdateSuccess, model.Name));
			}

			Value.UpdateFrom(model);
			await ValueChanged.InvokeAsync(this.Value);
		}
	}
}
