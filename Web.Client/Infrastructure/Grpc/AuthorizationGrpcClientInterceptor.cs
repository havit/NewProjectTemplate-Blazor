using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Havit.GoranG3.Web.Client.Infrastructure.Grpc
{
	public class AuthorizationGrpcClientInterceptor : Interceptor
	{
		public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
					TRequest request,
					ClientInterceptorContext<TRequest, TResponse> context,
					AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
		{
			var call = continuation(request, context);

			return new AsyncUnaryCall<TResponse>(HandleAsyncUnaryResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
		}

		private async Task<TResponse> HandleAsyncUnaryResponse<TResponse>(Task<TResponse> inputResponse)
		{
			try
			{
				return await inputResponse;
			}
			catch (RpcException e) when (e.Status.DebugException is AccessTokenNotAvailableException innerException)
			{
				innerException.Redirect();

				return default;
			}
		}
	}
}
