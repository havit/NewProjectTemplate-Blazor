using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Admin.Components;

public partial class DataSeeds : ComponentBase
{
	[Inject] protected Func<IDataSeedFacade> DataSeedFacade { get; set; }
	[Inject] protected NavigationManager NavigationManager { get; set; }
	[Inject] protected IHxMessageBoxService MessageBox { get; set; }

	private IEnumerable<string> seedProfiles;
	private string selectedSeedProfile;
	private HxOffcanvas offcanvasComponent;

	private async Task HandleSeedClick()
	{
		if ((selectedSeedProfile is not null) && await MessageBox.ConfirmAsync($"Do you really want to seed {selectedSeedProfile}?"))
		{
			await DataSeedFacade().SeedDataProfileAsync(Dto.FromValue(selectedSeedProfile));

			if (await MessageBox.ConfirmAsync($"Seed successful: {selectedSeedProfile}", "Seed was successful. Do you want to reload the Blazor client?"))
			{
				NavigationManager.NavigateTo("", forceLoad: true);
			}
			else
			{
				await offcanvasComponent.HideAsync();
			}
		}
	}

	public async Task ShowAsync()
	{
		seedProfiles ??= await DataSeedFacade().GetDataSeedProfilesAsync();

		await offcanvasComponent.ShowAsync();
	}
}
