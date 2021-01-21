using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Web.Server.Infrastructure.Interceptors
{
	public class ServerValidationErrorInterceptor : Interceptor
	{
		public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
		{
			try
			{
				return await continuation(request, context);
			}
			catch (InvalidOperationException e)
			{
				context.ResponseTrailers.Add("entityvalidationerror", e.Message);
				throw;
			}
		}
	}
}
