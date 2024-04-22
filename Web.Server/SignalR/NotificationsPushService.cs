using Havit.NewProjectTemplate.Contracts.Notifications;
using Havit.NewProjectTemplate.Services.Notifications;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Havit.NewProjectTemplate.Web.Server.SignalR;

public class NotificationsPushService : INotificationsPushService
{
	private readonly IHubContext<NotificationsHub> _hubContext;

	public NotificationsPushService(IHubContext<NotificationsHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task UpdateNotificationsCountAsync(NotificationsCountDto notificationsCount, CancellationToken cancellationToken)
	{
		await _hubContext.Clients.All.SendAsync("NotificationsCountUpdated", notificationsCount, cancellationToken);
	}
}
