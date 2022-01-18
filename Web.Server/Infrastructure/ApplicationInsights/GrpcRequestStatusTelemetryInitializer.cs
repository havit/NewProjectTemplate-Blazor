using Grpc.Core;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ApplicationInsights;

/// <summary>
/// Application Insights telemetry initializer responsible for setting gRPC requests as failed (based on grpc-status HTTP Header).
/// Also responsible for OperationFailedException-responses not marking the request as failed.
/// (They are not treated as failed by default as the HTTP Status code remains 200 (OK) even if there is an exception being passed to the client).
/// </summary>
/// <remarks>
/// https://docs.microsoft.com/en-us/azure/azure-monitor/app/api-filtering-sampling#addmodify-properties-itelemetryinitializer
/// </remarks>
public class GrpcRequestStatusTelemetryInitializer : ITelemetryInitializer
{
	private readonly IHttpContextAccessor httpContextAccessor;

	public GrpcRequestStatusTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
	{
		this.httpContextAccessor = httpContextAccessor;
	}

	public void Initialize(ITelemetry telemetry)
	{
		if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
		{
			telemetry.Context.Cloud.RoleName = "Web.Server";
			// telemetry.Context.Cloud.RoleInstance = "...";
		}

		var requestTelemetry = telemetry as RequestTelemetry;
		if (requestTelemetry == null)
		{
			return;
		}

		if (httpContextAccessor.HttpContext.Response.Headers.TryGetValue("grpc-status", out var grpcStatusHeader))
		{
			if (Enum.TryParse<StatusCode>(grpcStatusHeader[0], out var grpcStatusCode))
			{
				if ((grpcStatusCode != StatusCode.OK)
					&& (grpcStatusCode != StatusCode.FailedPrecondition))  // OperationFailedException
				{
					requestTelemetry.Success = false;
				}
			}
		}
	}
}
