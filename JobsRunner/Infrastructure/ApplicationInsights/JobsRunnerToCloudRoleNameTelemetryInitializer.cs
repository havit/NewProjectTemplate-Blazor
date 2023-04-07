using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Havit.NewProjectTemplate.JobsRunner.Infrastructure.ApplicationInsights;

/// <summary>
/// Pomáhá odlišit telemetrii hangfire jobů od http requestů webové aplikace, když obojí reportujeme jako RequestTelemetry
/// pomocí hodnoty cloud_RoleName.
/// (Řeší i nejen RequestTelemetry, ale veškerá telemetrická data z JobsRunner.)
/// </summary>
/// <remarks>
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
		else if (!telemetry.Context.Cloud.RoleName.EndsWith("JobsRunner")) // (zde není podtržítko, protože předchozí volání telemetry initializeru může nastavit RoleName na "JobsRunner" samotné)
		{
			telemetry.Context.Cloud.RoleName += "_JobsRunner";
		}
	}
}