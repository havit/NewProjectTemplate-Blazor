using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ApplicationInsights
{
	public class EnrichmentTelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			telemetry.Context.Cloud.RoleName = "Web.Server";
		}
	}
}