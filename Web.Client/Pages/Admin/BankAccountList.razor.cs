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
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class BankAccountList : ComponentBase
	{
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] protected IBankAccountLocalizer BankAccountLoc { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLoc { get; set; }

		private List<BankAccountDto> bankAccounts;
		private BankAccountDto bankAccountEdited = new BankAccountDto();
		private BankAccountEdit bankAccountEdit;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			bankAccounts = (await BankAccountFacade.GetBankAccountsAsync()).Value;
		}

		private async Task HandleSelectedDataItemChanged(BankAccountDto bankAccountSelected)
		{
			bankAccountEdited = bankAccountSelected;
			await bankAccountEdit.ShowAsync();
		}

		private async Task NewItemClicked()
		{
			bankAccountEdited = new BankAccountDto();
			await bankAccountEdit.ShowAsync();
		}

		private async Task DeleteItemClicked(BankAccountDto bankAccount)
		{
			await BankAccountFacade.DeleteBankAccountAsync(Dto.FromValue(bankAccount.Id));
			Messenger.AddInformation(bankAccount.Name, GlobalLoc.DeleteSuccess);
			await LoadDataAsync();
		}

		private async Task HandleValueChanged()
		{
			await LoadDataAsync();
		}
	}
}
