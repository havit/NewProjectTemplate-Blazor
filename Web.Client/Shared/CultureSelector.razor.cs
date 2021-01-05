using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Shared
{
	public partial class CultureSelector : ComponentBase
	{
		[Inject] protected ILocalStorageService LocalStorageService { get; set; }
		[Inject] protected NavigationManager NavigationManager { get; set; }

		private async Task SetCulture(string culture)
		{
			await LocalStorageService.SetItemAsync("culture", culture);
			NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
		}
	}
}
