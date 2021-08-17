using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Grpc.Client.ServerExceptions;
using Havit.NewProjectTemplate.Resources;
using Microsoft.Extensions.Localization;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc
{
	public class HxMessengerOperationFailedExceptionPublisher : IOperationFailedExceptionPublisher
	{
		private readonly IHxMessengerService messenger;
		private readonly IStringLocalizer<Global> localizer;

		public HxMessengerOperationFailedExceptionPublisher(IHxMessengerService messenger, IStringLocalizer<Global> localizer)
		{
			this.messenger = messenger;
			this.localizer = localizer;
		}
		public Task PublishAsync(string errorMessage)
		{
			messenger.AddError(localizer["OperationFailedExceptionMessengerTitle"], errorMessage);

			return Task.CompletedTask;
		}
	}
}
