using Blazored.LocalStorage;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Web.Client.Pages.Admin.Components;
using Havit.NewProjectTemplate.Web.Client.Resources;
using Havit.NewProjectTemplate.Web.Client.Resources.Pages.Admin;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Admin;

public partial class AdminIndex : ComponentBase
{
	[Inject] protected Func<IMaintenanceFacade> MaintenanceFacade { get; set; }
	[Inject] protected IHxMessengerService Messenger { get; set; }
	[Inject] protected IHxMessageBoxService MessageBox { get; set; }
	[Inject] protected ILocalStorageService LocalStorageService { get; set; }
	[Inject] protected INavigationLocalizer NavigationLocalizer { get; set; }
	[Inject] protected IAdminIndexLocalizer AdmninIndexLocalizer { get; set; }

	private DataSeeds dataSeedsComponent;

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
			await MaintenanceFacade().ClearCache();
			Messenger.AddInformation("Server cache cleared.");
		}
	}
}
