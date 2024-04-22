
using Havit.NewProjectTemplate.Contracts.Notifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Havit.NewProjectTemplate.Web.Client.Shared;

public partial class Sidebar
{
	[Inject] protected NavigationManager NavigationManager { get; set; }

	private HubConnection? _hubConnection;
	private int _notificationsCount;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await SubscribeToNotificationsCountUpdatesAsync();
		}
	}

	private async Task SubscribeToNotificationsCountUpdatesAsync()
	{
		_hubConnection = new HubConnectionBuilder()
			.WithUrl(NavigationManager.ToAbsoluteUri("/notifications-hub"))
			.Build();

		_hubConnection.On<NotificationsCountDto>("NotificationsCountUpdated", async (notificationsCountDto) =>
		{
			await InvokeAsync(() =>
			{
				Console.WriteLine(notificationsCountDto);
				_notificationsCount = notificationsCountDto.Count;
				StateHasChanged();
			});
		});

		await _hubConnection.StartAsync();
	}
}
