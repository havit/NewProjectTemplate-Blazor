using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Errors;

public partial class GenericError : IAsyncDisposable
{
	[Parameter] public Exception Exception { get; set; }
	[Parameter] public EventCallback OnRecover { get; set; }

	[Inject] private IJSRuntime JSRuntime { get; set; }

	private IJSObjectReference jsModule;

	private async Task EnsureJsModule()
	{
		jsModule ??= await JSRuntime.ImportModuleAsync("./js/GenericError.js");
	}

	private async Task CopyExceptionDetailsToClipboard()
	{
		await EnsureJsModule();

		string clipboardText = Exception.ToString();
		await jsModule.InvokeVoidAsync("copyToClipboard", clipboardText);
	}

	public async ValueTask DisposeAsync()
	{
		if (jsModule is not null)
		{
			await jsModule.DisposeAsync();
		}
	}
}
