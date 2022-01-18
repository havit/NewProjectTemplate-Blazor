using Havit.Blazor.Grpc.Client.ServerExceptions;
using Havit.NewProjectTemplate.Resources;
using Microsoft.Extensions.Localization;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;

public class HxMessengerOperationFailedExceptionGrpcClientListener : IOperationFailedExceptionGrpcClientListener
{
	private readonly IHxMessengerService messenger;
	private readonly IStringLocalizer<Global> localizer;

	public HxMessengerOperationFailedExceptionGrpcClientListener(IHxMessengerService messenger, IStringLocalizer<Global> localizer)
	{
		this.messenger = messenger;
		this.localizer = localizer;
	}

	public Task ProcessAsync(string errorMessage)
	{
		messenger.AddError(localizer["OperationFailedExceptionMessengerTitle"], errorMessage);

		return Task.CompletedTask;
	}
}
