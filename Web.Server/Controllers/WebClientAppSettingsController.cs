using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Controllers;

[AllowAnonymous]
public class WebClientAppSettingsController(
		IOptions<WebClientOptions> webClientOptions,
		TelemetryConfiguration telemetryConfiguration) : ControllerBase
{
	private readonly IOptions<WebClientOptions> _webClientOptions = webClientOptions;
	private readonly TelemetryConfiguration _telemetryConfiguration = telemetryConfiguration;

	[HttpGet(WebClientOptions.WebClientConfigurationRoute)]
	public WebClientOptions GetMainWebClientAppSettings()
	{
		var webClientOptionsValue = _webClientOptions.Value;

		// If the Application Insights Connection String for the client is specified in the configuration, use it.
		// Otherwise, use the connection string of the Application Insights server.
		return (!String.IsNullOrEmpty(webClientOptionsValue.ApplicationInsights?.ConnectionString))
			? webClientOptionsValue
			: webClientOptionsValue with
			{
				ApplicationInsights = new ApplicationInsightsOptions
				{
					ConnectionString = _telemetryConfiguration.ConnectionString
				}
			};
	}
}
