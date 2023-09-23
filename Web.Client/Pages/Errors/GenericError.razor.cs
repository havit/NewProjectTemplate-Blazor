using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Errors;

public partial class GenericError : IAsyncDisposable
{
	[Parameter] public Exception Exception { get; set; }
	[Parameter] public EventCallback OnRecover { get; set; }

	[Inject] public Resources.Pages.Errors.IGenericErrorLocalizer GenericErrorLocalizer { get; set; }
	[Inject] public NavigationManager NavigationManager { get; set; }
	[Inject] private IJSRuntime JSRuntime { get; set; }

	private IJSObjectReference jsModule;
	private DotNetObjectReference<GenericError> dotnetObjectReference;

	private HxTooltip copyToClipboardTooltip, copiedToClipboardTooltip;

	/// <summary>
	/// Indicates whether the exception's contents have been copied to clipboard.
	/// </summary>
	private bool copiedToClipboard = false;
	private string traceID;

	public GenericError()
	{
		dotnetObjectReference = DotNetObjectReference.Create(this);
	}

	protected override async Task OnInitializedAsync()
	{
		await EnsureJsModule();
		traceID = await jsModule.InvokeAsync<string>("getTraceID");
	}

	private void HandleRestartClick()
	{
		NavigationManager.NavigateTo("", forceLoad: true);
	}

	private async Task EnsureJsModule()
	{
		jsModule ??= await JSRuntime.ImportModuleAsync("./Pages/Errors/GenericError.razor.js");
	}

	private async Task CopyExceptionDetailsToClipboard()
	{
		await copyToClipboardTooltip.HideAsync();

		await EnsureJsModule();

		await jsModule.InvokeVoidAsync("copyToClipboard", GetFullExceptionText(), dotnetObjectReference);
	}

	private string GetFullExceptionText() => "Operation ID:" + traceID + Environment.NewLine + Exception.ToString();

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
