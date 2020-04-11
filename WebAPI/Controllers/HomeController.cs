using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Havit.GoranG3.WebAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)] // schováme ze Swaggeru
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
				// TODO: Security scheme
				throw new NotImplementedException();
	            //return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
            }
            return this.Redirect("swagger");
        }
    }
}
