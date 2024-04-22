using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Absences;
using Havit.NewProjectTemplate.Contracts.Notifications;
using Havit.NewProjectTemplate.Services.Notifications;

namespace Havit.NewProjectTemplate.Facades.Absences;

[Service]
public class AbsenceFacade : IAbsenceFacade
{
	private readonly INotificationsPushService _notificationsPushService;

	public AbsenceFacade(INotificationsPushService notificationsPushService)
	{
		_notificationsPushService = notificationsPushService;
	}

	public async Task CreateAbsenceAsync(CancellationToken cancellationToken)
	{
		// ...

		await _notificationsPushService.UpdateNotificationsCountAsync(new NotificationsCountDto { Count = Random.Shared.Next(100) }, cancellationToken);
	}
}
