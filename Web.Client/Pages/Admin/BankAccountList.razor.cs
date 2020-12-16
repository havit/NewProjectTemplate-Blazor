using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private List<BankAccountDto> bankAccounts;
		private BankAccountDto bankAccountEdited;
		private bool detailDrawerIsOpen;

		[Inject] public IMessenger Messenger { get; set; }
		[Inject] public IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] public IStringLocalizer<BankAccountResources> BankAccountLoc { get; set; }

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
			Messenger.AddInformation(String.Format(BankAccountLoc["DeleteSuccess"], bankAccount.Name));
			await LoadDataAsync();
		}
	}
}
