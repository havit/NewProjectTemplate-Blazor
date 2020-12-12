using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	// TODO: Lokální Value instance se změní, i když změny nejsou uloženy na server.
	// TODO: Detekce změn + onBeforeUnload? nebo alespoň před přepnutím na jinou instanci?
	public partial class BankAccountEdit : ComponentBase
	{
		[Parameter] public BankAccountDto Value { get; set; }
		[Parameter] public EventCallback<BankAccountDto> ValueChanged { get; set; }
		[CascadingParameter] public IMessenger Messenger { get; set; }

		[Inject] public IBankAccountFacade BankAccountFacade { get; set; }

		public async Task HandleValidSubmit()
		{
			if (Value.Id == default)
			{
				Value.Id = (await BankAccountFacade.CreateBankAccountAsync(Value)).Value;
				Messenger.AddInformation($"{Value.Name} created.");
			}
			else
			{
				await BankAccountFacade.UpdateBankAccountAsync(Value);
				Messenger.AddInformation($"{Value.Name} updated.");
			}
			await ValueChanged.InvokeAsync(this.Value);
		}
	}
}
