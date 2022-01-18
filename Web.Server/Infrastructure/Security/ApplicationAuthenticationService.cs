using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.DataLayer.Repositories.Security;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

/// <summary>
/// Poskytuje uživatele z HttpContextu.
/// </summary>
public class ApplicationAuthenticationService : IApplicationAuthenticationService
{
	private readonly IHttpContextAccessor httpContextAccessor;

	private readonly Lazy<User> userLazy;

	public ApplicationAuthenticationService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
	{
		this.httpContextAccessor = httpContextAccessor;

		userLazy = new Lazy<User>(() => userRepository.GetObject(GetCurrentUserId()));
	}

	public ClaimsPrincipal GetCurrentClaimsPrincipal()
	{
		return httpContextAccessor.HttpContext.User;
	}

	public User GetCurrentUser() => userLazy.Value;

	public int GetCurrentUserId()
	{
		var principal = GetCurrentClaimsPrincipal();
		Claim userIdClaim = principal.Claims.Single(claim => (claim.Type == "sub"));
		return Int32.Parse(userIdClaim.Value);
	}
}
