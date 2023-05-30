using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Havit.NewProjectTemplate.JobsRunner.Infrastructure.ApplicationInsights;

/// <summary>
/// Helps distinguish telemetry of hangfire jobs from http requests (both reported as RequestTelemetry)
/// <remarks>
/// </summary>
/// Custom ITelemetryInitializer being called multiple times: https://github.com/microsoft/ApplicationInsights-dotnet-server/issues/977
/// </remarks>
public class JobsRunnerToCloudRoleNameTelemetryInitializer : ITelemetryInitializer
{
	public void Initialize(ITelemetry telemetry)
	{
		if (String.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
		{
			telemetry.Context.Cloud.RoleName = "JobsRunner";
		}
		else if (!telemetry.Context.Cloud.RoleName.EndsWith("JobsRunner"))
		{
			telemetry.Context.Cloud.RoleName += "_JobsRunner";
		}
	}
}