using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Shared;

// zdroj: https://github.com/dotnet/blazor-samples/tree/main/8.0/BlazorWebAppOidc

public partial class RedirectToLogin
{
	[Inject] public NavigationManager NavigationManager { get; set; }

	protected override void OnInitialized()
	{
		NavigationManager.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}", forceLoad: true);
	}
}

