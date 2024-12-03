using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Antiforgery;

// Replicates EndpointAntiforgeryStateProvider (incl. base DefaultAntiforgeryStateProvider) and adds a workaround for the issue with the ResourceCollectionProvider
// https://github.com/dotnet/aspnetcore/issues/58822
public class WorkaroundEndpointAntiforgeryStateProvider : AntiforgeryStateProvider, IDisposable
{
	private const string PersistenceKey = $"__internal__{nameof(AntiforgeryRequestToken)}";
	private readonly PersistingComponentStateSubscription _subscription;
	private readonly PersistingComponentStateSubscription _subscriptionDummy;
	private readonly AntiforgeryRequestToken _currentToken;
	private readonly IAntiforgery _antiforgery;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly PersistentComponentState _state;

	public WorkaroundEndpointAntiforgeryStateProvider(
		IAntiforgery antiforgery,
		IHttpContextAccessor httpContextAccessor,
		PersistentComponentState state)
	{
		// This is a dummy subscription that does nothing. It's only here as the previous ResourceCollectionProvider
		// persising callback disposes its subscription, which modifies the ComponentStatePersistenceManager._registeredCallbacks
		// collection in while looping and causing next callback to be skipped.
		_subscriptionDummy = state.RegisterOnPersisting(() => Task.CompletedTask, RenderMode.InteractiveWebAssembly);

		// Automatically flow the Request token to server/wasm through
		// persistent component state. This guarantees that the antiforgery
		// token is available on the interactive components, even when they
		// don't have access to the request.
		_subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);


		state.TryTakeFromJson(PersistenceKey, out _currentToken);
		_antiforgery = antiforgery;
		_httpContextAccessor = httpContextAccessor;
		_state = state;
	}

	private Task OnPersistingAsync()
	{
		_state.PersistAsJson(PersistenceKey, GetAntiforgeryToken());
		return Task.CompletedTask;
	}

	public override AntiforgeryRequestToken GetAntiforgeryToken()
	{
		if (_httpContextAccessor.HttpContext == null)
		{
			// We're in an interactive context. Use the token persisted during static rendering.
			return _currentToken;
		}


		// We already have a callback setup to generate the token when the response starts if needed.
		// If we need the tokens before we start streaming the response, we'll generate and store them;
		// otherwise we'll just retrieve them.
		// In case there are no tokens available, we are going to return null and no-op.
		var tokens = !_httpContextAccessor.HttpContext.Response.HasStarted ? _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext) : _antiforgery.GetTokens(_httpContextAccessor.HttpContext);
		if (tokens.RequestToken is null)
		{
			return null;
		}


		return new AntiforgeryRequestToken(tokens.RequestToken, tokens.FormFieldName);
	}


	/// <inheritdoc />
	public void Dispose()
	{
		_subscriptionDummy.Dispose();
		_subscription.Dispose();
	}
}
