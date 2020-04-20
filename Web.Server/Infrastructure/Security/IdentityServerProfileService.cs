using Havit.GoranG3.Model.Security;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Server.Infrastructure.Security
{
	/// <summary>
	/// Adding roles to claims returned by IdentityServer.
	/// </summary>
	public class IdentityServerProfileService : IProfileService
	{
		private readonly UserManager<User> userManager;

		public IdentityServerProfileService(UserManager<User> userManager)
		{
			this.userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var user = await userManager.GetUserAsync(context.Subject);
			var roles = await userManager.GetRolesAsync(user);

			var claims = roles.Select(role => new Claim(JwtClaimTypes.Role, role));
			context.IssuedClaims.AddRange(claims);
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var user = await userManager.GetUserAsync(context.Subject);
			context.IsActive = (user != null) && user.LockoutEnabled;
		}
	}
}
