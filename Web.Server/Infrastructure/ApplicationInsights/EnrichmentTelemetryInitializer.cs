using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ApplicationInsights;

public class EnrichmentTelemetryInitializer : ITelemetryInitializer
{
	public void Initialize(ITelemetry telemetry)
	{
		telemetry.Context.Cloud.RoleName = "Web.Server";
	}
}
