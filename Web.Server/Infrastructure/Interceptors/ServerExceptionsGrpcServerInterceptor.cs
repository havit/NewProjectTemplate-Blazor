using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Havit;
using Havit.Data.Patterns.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Configuration;

namespace Havit.GoranG3.Web.Server.Infrastructure.Interceptors
{
	// DI SINGLETON !!
	public class ServerExceptionsGrpcServerInterceptor : ServerExceptionsInterceptorBase
	{
		private readonly ILogger<ServerExceptionsGrpcServerInterceptor> logger;

		public ServerExceptionsGrpcServerInterceptor(ILogger<ServerExceptionsGrpcServerInterceptor> logger)
		{
			this.logger = logger;
		}

		protected override bool OnException(Exception exception, out Status status)
		{
			if (exception is RpcException)
			{
				status = default;
				return false;
			}

			if (exception is OperationFailedException)
			{
				status = new Status(StatusCode.FailedPrecondition, // see ServerExceptionsGrpcClientInterceptor - gets propagated to HxMessenger + client-side OperationFailedException
									exception.Message);

				logger.LogInformation(exception, exception.Message); // e.g. for ApplicationInsights (where Warning and higher levels get tracked by default)
			}
			else
			{
				status = new Status(exception switch
				{
					NotImplementedException => StatusCode.Unimplemented,
					SecurityException => StatusCode.PermissionDenied,
					ArgumentOutOfRangeException => StatusCode.OutOfRange,
					ArgumentException or ArgumentNullException => StatusCode.InvalidArgument,
					OperationCanceledException => StatusCode.Cancelled,
					ObjectNotFoundException => StatusCode.NotFound,
					TimeoutException => StatusCode.DeadlineExceeded,

					_ => StatusCode.Unknown,

				},
#if DEBUG
				exception.ToString());
#else
				$"{exception.GetType().FullName}: {exception.Message}");
#endif

				logger.LogError(exception, exception.Message); // DO NOT REMOVE - passes exception to ApplicationInsights tracking (Warning and higher levels get tracked by default)
			}
			return true;
		}
	}
}
