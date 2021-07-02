using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Grpc
{
	// DI SINGLETON !!
	public class GlobalizationLocalizationGrpcServerInterceptor : Interceptor
	{
		private void SetCultureFromMetadata(ServerCallContext context)
		{
			var cultureInfoName = context.RequestHeaders.SingleOrDefault(h => h.Key == "hx-culture")?.Value;
			if (!String.IsNullOrWhiteSpace(cultureInfoName))
			{
				var cultureInfo = new CultureInfo(cultureInfoName);
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
			}
		}

		public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
			  TRequest request,
			  ServerCallContext context,
			  UnaryServerMethod<TRequest, TResponse> continuation)
		{
			SetCultureFromMetadata(context);

			return base.UnaryServerHandler(request, context, continuation);
		}

		public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
			IAsyncStreamReader<TRequest> requestStream,
			ServerCallContext context,
			ClientStreamingServerMethod<TRequest, TResponse> continuation)
		{
			SetCultureFromMetadata(context);

			return base.ClientStreamingServerHandler(requestStream, context, continuation);
		}

		public override Task ServerStreamingServerHandler<TRequest, TResponse>(
			TRequest request,
			IServerStreamWriter<TResponse> responseStream,
			ServerCallContext context,
			ServerStreamingServerMethod<TRequest, TResponse> continuation)
		{
			SetCultureFromMetadata(context);

			return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
		}

		public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
			IAsyncStreamReader<TRequest> requestStream,
			IServerStreamWriter<TResponse> responseStream,
			ServerCallContext context,
			DuplexStreamingServerMethod<TRequest, TResponse> continuation)
		{
			SetCultureFromMetadata(context);

			return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
		}
	}
}
