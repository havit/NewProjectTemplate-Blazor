using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havit.GoranG3.WebAPI.Infrastructure.Security
{
	public class OpenIdConnectSettings
	{
		public string Authority { get; set; }
		public string ClientId { get; set; }
		public string Scope { get; set; }
		public string ClientSecret { get; set; }
	}
}
