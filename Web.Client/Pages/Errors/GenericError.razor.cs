using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Errors;

public partial class GenericError : IAsyncDisposable
{
	[Parameter] public Exception Exception { get; set; }
	[Parameter] public EventCallback OnRecover { get; set; }

	[Inject] private IJSRuntime JSRuntime { get; set; }

	private IJSObjectReference jsModule;
	private DotNetObjectReference<GenericError> dotnetObjectReference;

	private HxTooltip copyToClipboardTooltip, copiedToClipboardTooltip;

	/// <summary>
	/// Indicates whether the exception's contents have been copied to clipboard.
	/// </summary>
	private bool copiedToClipboard = false;

	public GenericError()
	{
		dotnetObjectReference = DotNetObjectReference.Create(this);
	}

	private async Task EnsureJsModule()
	{
		jsModule ??= await JSRuntime.ImportModuleAsync("./js/GenericError.js");
	}

	private async Task CopyExceptionDetailsToClipboard()
	{
		await copyToClipboardTooltip.HideAsync();

		await EnsureJsModule();

		string clipboardText = Exception.ToString();
		await jsModule.InvokeVoidAsync("copyToClipboard", clipboardText, dotnetObjectReference);
	}

	[JSInvokable("GenericError_HandleCopiedToClipboard")]
	public async Task HandleCopiedToClipboard()
	{
		copiedToClipboard = true;
		StateHasChanged();

		await copiedToClipboardTooltip.ShowAsync();
	}

	public async ValueTask DisposeAsync()
	{
		if (dotnetObjectReference is not null)
		{
			dotnetObjectReference.Dispose();
		}

		if (jsModule is not null)
		{
			await jsModule.DisposeAsync();
		}
	}
}
