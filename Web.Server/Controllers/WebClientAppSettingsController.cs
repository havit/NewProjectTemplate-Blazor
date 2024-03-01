using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Controllers;

[AllowAnonymous]
public class WebClientAppSettingsController(
	IOptions<WebClientOptions> _webClientOptions,
	TelemetryConfiguration _telemetryConfiguration) : ControllerBase
{
	[HttpGet(WebClientOptions.WebClientConfigurationRoute)]
	public WebClientOptions GetMainWebClientAppSettings()
	{
		var webClientOptionsValue = _webClientOptions.Value;

		// Pokud je zadán Application Insights Connection String pro klienta v konfiguraci, použijeme tento.
		// V ostatních případech se použije connection string Application Insights serveru.
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
