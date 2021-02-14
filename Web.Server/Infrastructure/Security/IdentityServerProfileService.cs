using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Model.Security;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security
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
			context.AddRequestedClaims(context.Subject.Claims);

			// copy "role" claims
			// TODO might be determined by the IdentityServer configuration?
			context.IssuedClaims.AddRange(context.Subject.Claims.Where(c => c.Type == JwtClaimTypes.Role));

			var user = await userManager.GetUserAsync(context.Subject);
			context.IssuedClaims.Add(new Claim(JwtClaimTypes.NickName, user.DisplayName ?? user.Username));
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var user = await userManager.GetUserAsync(context.Subject);
			context.IsActive = (user != null) && user.LockoutEnabled;
		}
	}
}
