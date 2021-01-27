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
	public partial class BankAccountList
	{
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IBankAccountFacade BankAccountFacade { get; set; }
		[Inject] protected IBankAccountLocalizer BankAccountLocalizer { get; set; }
		[Inject] protected IGlobalLocalizer GlobalLocalizer { get; set; }

		private BankAccountDto bankAccountSelected;
		private BankAccountDto bankAccountEdited = new BankAccountDto();
		private HxGrid<BankAccountDto> bankAccountsGrid;
		private BankAccountEdit bankAccountEditComponent;

		private async Task<GridDataProviderResult<BankAccountDto>> LoadBankAccounts(GridDataProviderRequest<BankAccountDto> request)
		{
			return request.ApplyTo((await BankAccountFacade.GetBankAccountsAsync()).Value);
		}

		private async Task HandleSelectedDataItemChanged(BankAccountDto selection)
		{
			bankAccountSelected = selection;
			bankAccountEdited = selection;
			await bankAccountEditComponent.ShowAsync();
		}

		private async Task NewItemClicked()
		{
			bankAccountEdited = new BankAccountDto();
			await bankAccountEditComponent.ShowAsync();
		}

		private async Task DeleteItemClicked(BankAccountDto bankAccount)
		{
			await BankAccountFacade.DeleteBankAccountAsync(Dto.FromValue(bankAccount.Id));
			Messenger.AddInformation(bankAccount.Name, GlobalLocalizer.DeleteSuccess);
			await bankAccountsGrid.RefreshDataAsync();
		}

		private async Task HandleEditValueChanged()
		{
			await bankAccountsGrid.RefreshDataAsync();
		}

		private void HandleEditClosed()
		{
			bankAccountSelected = null;
			bankAccountEdited = new BankAccountDto();
		}
	}
}
