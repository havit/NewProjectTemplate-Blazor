using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Admin.Components;

public partial class DataSeeds : ComponentBase
{
	[Inject] protected IDataSeedFacade DataSeedFacade { get; set; }
	[Inject] protected NavigationManager NavigationManager { get; set; }
	[Inject] protected IHxMessageBoxService MessageBox { get; set; }

	private IEnumerable<string> _seedProfiles;
	private string _selectedSeedProfile;
	private HxOffcanvas _offcanvasComponent;

	private async Task HandleSeedClick()
	{
		if ((_selectedSeedProfile is not null) && await MessageBox.ConfirmAsync($"Do you really want to seed {_selectedSeedProfile}?"))
		{
			await DataSeedFacade.SeedDataProfileAsync(Dto.FromValue(_selectedSeedProfile));

			if (await MessageBox.ConfirmAsync($"Seed successful: {_selectedSeedProfile}", "Seed was successful. Do you want to reload the Blazor client?"))
			{
				NavigationManager.NavigateTo("", forceLoad: true);
			}
			else
			{
				await _offcanvasComponent.HideAsync();
			}
		}
	}

	public async Task ShowAsync()
	{
		_seedProfiles ??= await DataSeedFacade.GetDataSeedProfilesAsync();

		await _offcanvasComponent.ShowAsync();
	}
}
