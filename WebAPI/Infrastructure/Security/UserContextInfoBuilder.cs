using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Havit.GoranG3.Facades.Infrastructure.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Havit.GoranG3.WebAPI.Infrastructure.Security
{
    public class UserContextInfoBuilder : IUserContextInfoBuilder
	{
		/// <summary>
		/// Vrací informace o přihlášeném uživateli.
		/// Pro nepřihlášeného uživatele vrací null.
		/// </summary>
		public UserContextInfo GetUserContextInfo(ClaimsPrincipal principal)
		{
			// nelze použít httpContextAccessor.HttpContext.User.Identity.IsAuthenticated, protože jeho Identity.IsAuthenticated v tento okamžik 
			// ještě false (ačkoliv pro principal z parametru je true).
			if (!principal.Identity.IsAuthenticated)
			{
				return null;
			}

			if (userContextInfo == null)
			{
				// vyzvedneme claim s username; nelze principal.Identity.Name, který je v daný okamžik ještě null
				Claim usernameClaim = principal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);

				userContextInfo = new UserContextInfo(username: usernameClaim.Value);
			}

			return userContextInfo;
		}

		private UserContextInfo userContextInfo;
	}
}
