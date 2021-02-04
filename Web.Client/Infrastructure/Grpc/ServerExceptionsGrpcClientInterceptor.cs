using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Havit.GoranG3.Web.Client.Infrastructure.Interceptors
{
	public class ServerExceptionsGrpcClientInterceptor : Interceptor
	{
		// do not inject scoped services here, the scope is not available

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
			catch (RpcException e)
			{
				// This is where we throw the OperationFailedException received from server. The HxMessenger stuff is in ApplicationGrpcWebHandler.
				Metadata.Entry validationTrailer = e.Trailers.Get(nameof(OperationFailedException).ToLower());
				if (validationTrailer != null)
				{
					throw new OperationFailedException(validationTrailer.Value);
				}

				throw;
			}
		}
	}
}
