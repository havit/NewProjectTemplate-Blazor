using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Contracts.Notifications;

namespace Havit.NewProjectTemplate.Services.Notifications;

public interface INotificationsPushService
{
	Task UpdateNotificationsCountAsync(NotificationsCountDto notificationsCount, CancellationToken cancellationToken);
}
