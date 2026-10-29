using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Controllers;

[AllowAnonymous]
public class WebClientAppSettingsController(IOptions<WebClientOptions> webClientOptions) : ControllerBase
{
	[HttpGet(WebClientOptions.WebClientConfigurationRoute)]
	public WebClientOptions GetMainWebClientAppSettings()
	{
		return webClientOptions.Value;
	}
}
