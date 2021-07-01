using System;
using Hangfire.Common;
using Hangfire.Server;
using Havit.AspNetCore.ExceptionMonitoring.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.UverovaPlatforma.Utility.Hangfire
{
	public class ExceptionMonitoringAttribute : JobFilterAttribute, IServerFilter
	{
		private readonly IServiceProvider serviceProvider;

		public ExceptionMonitoringAttribute(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void OnPerforming(PerformingContext filterContext)
		{
			// NOOP
		}

		public void OnPerformed(PerformedContext filterContext)
		{
			if ((filterContext.Exception != null) && !filterContext.ExceptionHandled)
			{
				var service = serviceProvider.GetRequiredService<IExceptionMonitoringService>();
				service.HandleException(filterContext.Exception.InnerException ?? filterContext.Exception);
			}
		}
	}
}
