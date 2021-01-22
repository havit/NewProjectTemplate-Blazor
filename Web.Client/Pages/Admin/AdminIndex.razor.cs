using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.System;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Resources.Pages.Admin;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class AdminIndex : ComponentBase
	{
		[Inject] protected IMaintenanceFacade MaintenanceFacade { get; set; }
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IHxMessageBoxService MessageBox { get; set; }
		[Inject] protected ILocalStorageService LocalStorageService { get; set; }
		[Inject] protected INavigationLocalizer NavigationLocalizer { get; set; }
		[Inject] protected IAdminIndexLocalizer AdmninIndexLocalizer { get; set; }

		private bool dataSeedsDrawerOpen = false;

		private async Task RemoveCultureFromLocalStorage()
		{
			if (await MessageBox.ConfirmAsync("Do you really want to remove culture cache?"))
			{
				await LocalStorageService.RemoveItemAsync("culture");
				Messenger.AddInformation(AdmninIndexLocalizer["CultureRemoved"]); // TODO Just a demo
			}
		}

		private async Task HandleClearCache()
		{
			if (await MessageBox.ConfirmAsync("Do you really want to clear server cache?"))
			{
				await MaintenanceFacade.ClearCache();
				Messenger.AddInformation("Server cache cleared.");
			}
		}
	}
}
