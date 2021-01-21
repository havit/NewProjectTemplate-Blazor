using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Havit.Blazor.Components.Web;

namespace Havit.GoranG3.Web.Client.Infrastructure.Interceptors
{
	public class ServerValidationErrorInterceptor : Interceptor
	{
		/*private readonly IHxMessengerService messengerService;

		public ServerValidationErrorInterceptor(
			IHxMessengerService messengerService)
		{
			this.messengerService = messengerService;
		}*/

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
				Metadata.Entry validationTrailer = e.Trailers.Get("entityvalidationerror");
				if (validationTrailer != null)
				{
					throw new InvalidOperationException(validationTrailer.Value);
				}

				throw;
			}
		}
	}
}
