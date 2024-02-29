using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Errors;

public partial class GenericError : IAsyncDisposable
{
	[Parameter] public Exception Exception { get; set; }
	[Parameter] public EventCallback OnRecover { get; set; }

	[Inject] public Resources.Pages.Errors.IGenericErrorLocalizer GenericErrorLocalizer { get; set; }
	[Inject] public NavigationManager NavigationManager { get; set; }
	[Inject] private IJSRuntime JsRuntime { get; set; }

	private IJSObjectReference _jsModule;
	private DotNetObjectReference<GenericError> _dotnetObjectReference;

	private HxTooltip _copyToClipboardTooltip, _copiedToClipboardTooltip;

	/// <summary>
	/// Indicates whether the exception's contents have been copied to clipboard.
	/// </summary>
	private bool _copiedToClipboard = false;
	private string _traceId;

	public GenericError()
	{
		_dotnetObjectReference = DotNetObjectReference.Create(this);
	}

	protected override async Task OnInitializedAsync()
	{
		await EnsureJsModuleAsync();
		_traceId = await _jsModule.InvokeAsync<string>("getTraceID");
	}

	private void HandleRestartClick()
	{
		NavigationManager.NavigateTo("", forceLoad: true);
	}

	private async Task EnsureJsModuleAsync()
	{
		_jsModule ??= await JsRuntime.ImportModuleAsync("./Pages/Errors/GenericError.razor.js");
	}

	private async Task HandleCopyExceptionDetailsToClipboardClick()
	{
		await _copyToClipboardTooltip.HideAsync();

		await EnsureJsModuleAsync();

		await _jsModule.InvokeVoidAsync("copyToClipboard", GetFullExceptionText(), _dotnetObjectReference);
	}

	private string GetFullExceptionText() => "Operation ID:" + _traceId + Environment.NewLine + Exception.ToString();

	[JSInvokable("GenericError_HandleCopiedToClipboard")]
	public async Task HandleCopiedToClipboard()
	{
		_copiedToClipboard = true;
		StateHasChanged();

		await _copiedToClipboardTooltip.ShowAsync();
	}

	public async ValueTask DisposeAsync()
	{
		_dotnetObjectReference?.Dispose();

		if (_jsModule is not null)
		{
			await _jsModule.DisposeAsync();
		}
	}
}
