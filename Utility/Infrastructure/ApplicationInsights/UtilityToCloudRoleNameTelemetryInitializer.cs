using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Havit.NewProjectTemplate.Utility.Infrastructure.ApplicationInsights
{
	/// <summary>
	/// Pomáhá odlišit telemetrii hangfire jobů od http requestů webové aplikace, když obojí reportujeme jako RequestTelemetry
	/// pomocí hodnoty cloud_RoleName.
	/// (Řeší i nejen RequestTelemetry, ale veškerá telemetrická data z Utility.)
	/// </summary>
	public class UtilityToCloudRoleNameTelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			if (String.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
			{
				telemetry.Context.Cloud.RoleName = "utility";
			}
			else
			{
				telemetry.Context.Cloud.RoleName += "_utility";
			}
		}
	}
}