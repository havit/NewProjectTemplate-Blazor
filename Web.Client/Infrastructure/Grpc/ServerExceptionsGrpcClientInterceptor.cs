using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.NewProjectTemplate.Resources;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Interceptors
{
	public class ServerExceptionsGrpcClientInterceptor : Interceptor
	{
		private readonly IHxMessengerService messenger;
		private readonly IStringLocalizer<Global> localizer;
		private readonly ILogger<ServerExceptionsGrpcClientInterceptor> logger;

		// do not inject scoped services here, the scope is not available
		public ServerExceptionsGrpcClientInterceptor(
			IHxMessengerService messenger,
			IStringLocalizer<Global> localizer,
			ILogger<ServerExceptionsGrpcClientInterceptor> logger)
		{
			this.messenger = messenger;
			this.localizer = localizer;
			this.logger = logger;
		}

		public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
					TRequest request,
					ClientInterceptorContext<TRequest, TResponse> context,
					AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
		{
			var call = continuation(request, context);

			return new AsyncUnaryCall<TResponse>(HandleResponseAsync(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
		}

		public async Task<TResponse> HandleResponseAsync<TResponse>(Task<TResponse> responseTask)
		{
			try
			{
				return await responseTask;
			}
			catch (RpcException e) when (e.Status.StatusCode == StatusCode.Cancelled)
			{
				throw (e.Status.DebugException as OperationCanceledException) ?? new OperationCanceledException(e.Message);
			}
			catch (RpcException e) when ((e.Status.StatusCode == StatusCode.Internal) && (e.Status.DebugException is HttpRequestException hre) && hre.Message.StartsWith("AbortError"))
			{
				throw new OperationCanceledException(hre.Message);
			}
			catch (RpcException e) when (e.Status.StatusCode == StatusCode.FailedPrecondition)
			{
				string errorMessage = e.Status.Detail;

				logger.LogWarning($"{nameof(OperationFailedException)}: {errorMessage}");

				messenger.AddError(localizer["OperationFailedExceptionMessengerTitle"], errorMessage);

				throw new OperationFailedException(errorMessage);
			}
		}
	}
}
