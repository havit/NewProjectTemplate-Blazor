using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Components.Web.Messenger;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Pages.Admin
{
	public partial class AdminIndex : ComponentBase
	{
		[Inject] protected IHxMessengerService Messenger { get; set; }
		[Inject] protected ILocalStorageService LocalStorageService { get; set; }
		[Inject] protected IStringLocalizer<AdminIndex> Loc { get; set; }

		private bool dataSeedsDrawerOpen = false;

		private async Task RemoveCultureFromLocalStorage()
		{
			await LocalStorageService.RemoveItemAsync("culture");
			Messenger.AddInformation(Loc["CultureRemoved"]);
		}
	}
}
