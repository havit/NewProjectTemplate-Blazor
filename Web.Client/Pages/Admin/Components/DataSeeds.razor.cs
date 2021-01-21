using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.System;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Admin.Components
{
	public partial class DataSeeds : ComponentBase
	{
		[Inject] protected IDataSeedFacade DataSeedFacade { get; set; }
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected IHxMessageBoxService MessageBox { get; set; }

		private IEnumerable<string> seedProfiles;
		private string selectedSeedProfile;

		protected override async Task OnInitializedAsync()
		{
			this.seedProfiles = (await DataSeedFacade.GetDataSeedProfiles()).Value;
		}

		private async Task HandleSeedClick()
		{
			if (selectedSeedProfile is not null && await MessageBox.ConfirmAsync($"Do you really want to seed {selectedSeedProfile}?"))
			{
				await DataSeedFacade.SeedDataProfile(selectedSeedProfile);
				Messenger.AddInformation($"Seed successful: {selectedSeedProfile}");
			}
		}
	}
}
