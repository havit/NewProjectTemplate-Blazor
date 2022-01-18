using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace Havit.NewProjectTemplate.Web.Server.Controllers;

public class OidcConfigurationController : Controller
{
	private readonly IClientRequestParametersProvider clientRequestParametersProvider;

	public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider)
	{
		this.clientRequestParametersProvider = clientRequestParametersProvider;
	}

	[HttpGet("_configuration/{clientId}")]
	public IActionResult GetClientRequestParameters([FromRoute] string clientId)
	{
		var parameters = clientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
		return Ok(parameters);
	}
}
