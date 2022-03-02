using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Havit.NewProjectTemplate.Model.Security;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

/// <summary>
/// Adding roles and other claims to claims returned by IdentityServer.
/// https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-identity-server?view=aspnetcore-5.0&tabs=visual-studio#profile-service
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

		context.IssuedClaims.AddRange(context.Subject.Claims.Where(c => c.Type == JwtClaimTypes.Role));

		var user = await userManager.GetUserAsync(context.Subject);
		context.IssuedClaims.Add(new Claim(JwtClaimTypes.NickName, user.DisplayName ?? user.Username));
	}

	public async Task IsActiveAsync(IsActiveContext context)
	{
		var user = await userManager.GetUserAsync(context.Subject);
		context.IsActive = (user != null)
							&& !user.Disabled
							&& (user.Deleted is null);
	}
}
