using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class AdminIndex : ComponentBase
	{
		[Inject] public IMessenger Messenger { get; set; }
		[Inject] public ILocalStorageService LocalStorageService { get; set; }
		[Inject] public IStringLocalizer<AdminIndex> Loc { get; set; }

		private bool dataSeedsDrawerOpen = false;

		private async Task RemoveCultureFromLocalStorage()
		{
			await LocalStorageService.RemoveItemAsync("culture");
			Messenger.AddInformation(Loc["CultureRemoved"]);
		}
	}
}
