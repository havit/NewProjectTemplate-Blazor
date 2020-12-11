using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.Finance;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class BankAccountList : ComponentBase
	{
		private List<BankAccountDto> bankAccounts;
		private BankAccountDto bankAccountEdited;
		private bool detailDrawerIsOpen;

		[CascadingParameter] public IMessenger Messenger { get; set; }

		[Inject] public IBankAccountFacade BankAccountFacade { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			bankAccounts = (await BankAccountFacade.GetBankAccountsAsync()).Value;
		}

		private void DataItemSelected(BankAccountDto bankAccountSelected)
		{
			bankAccountEdited = bankAccountSelected;
			detailDrawerIsOpen = true;
		}

		private void NewItemClicked()
		{
			bankAccountEdited = new BankAccountDto();
			detailDrawerIsOpen = true;
		}

		private async Task DeleteItemClicked(BankAccountDto bankAccount)
		{
			await BankAccountFacade.DeleteBankAccountAsync(Dto.FromValue(bankAccount.Id));
			Messenger.AddInformation(bankAccount.Name + " account deleted.");
			await LoadDataAsync();
		}
	}
}
