using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Havit;
using Havit.Data.Patterns.Exceptions;
using ProtoBuf.Grpc.Configuration;

namespace Web.Server.Infrastructure.Interceptors
{
	// DI SINGLETON !!
	public class ServerExceptionsGrpcServerInterceptor : ServerExceptionsInterceptorBase
	{
		protected override bool OnException(Exception exception, out Status status)
		{
			if (exception is RpcException)
			{
				status = default;
				return false;
			}

			status = new Status(exception switch
			{
				OperationFailedException => StatusCode.FailedPrecondition, // see ServerExceptionsGrpcClientInterceptor - gets propagated to HxMessenger + client-side OperationFailedException

				NotImplementedException => StatusCode.Unimplemented,
				SecurityException => StatusCode.PermissionDenied,
				ArgumentOutOfRangeException => StatusCode.OutOfRange,
				ArgumentException or ArgumentNullException => StatusCode.InvalidArgument,
				OperationCanceledException => StatusCode.Cancelled,
				ObjectNotFoundException => StatusCode.NotFound,
				TimeoutException => StatusCode.DeadlineExceeded,

				_ => StatusCode.Unknown,

			}, exception.Message);

			return true;
		}
	}
}
