using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Claims
{
	public interface IUserContextInfoBuilder
	{
		UserContextInfo GetUserContextInfo(ClaimsPrincipal principal);
	}
}
