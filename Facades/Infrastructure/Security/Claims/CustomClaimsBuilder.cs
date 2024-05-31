using System.Security;
using System.Security.Claims;
using System.Threading;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure.Security;
using Havit.NewProjectTemplate.DataLayer.Repositories.Security;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Security;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Services.Caching;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

[Service(Profile = ServiceProfiles.WebServer)]
public class CustomClaimsBuilder : ICustomClaimsBuilder
{
	private readonly IUserRepository _userRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;

	public CustomClaimsBuilder(
		IUserRepository userRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService)
	{
		_userRepository = userRepository;
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
	}

	public async Task<List<Claim>> GetCustomClaimsAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default)
	{
		Contract.Requires<SecurityException>(principal.Identity.IsAuthenticated);

		// All claims must be created with ClaimConstants.ApplicationIssuer because it is used as a selector by CookieOidcRefresher
		// when creating a new ("refreshed") principal.

		List<Claim> result = new();

		var user = await _userRepository.GetByIdentityProviderIdAsync(principal.FindFirst("oid").Value, cancellationToken);

		if (user == null)
		{
#if DEBUG
			user = await OnboardFirstUserAsync(principal, cancellationToken);
#endif
			if (user == null)
			{
				throw new SecurityException("User not found.");
			}
		}

		if (user.Disabled)
		{
			throw new SecurityException("User is disabled.");
		}

		if (user.Deleted is not null)
		{
			throw new SecurityException("User is deleted.");
		}

		result.Add(new Claim(ClaimConstants.UserIdClaim, user.Id.ToString(), null, ClaimConstants.ApplicationIssuer));

		var roles = user.UserRoles.Select(ur => (RoleEntry)ur.RoleId);
		foreach (var role in roles)
		{
			result.Add(new Claim(ClaimTypes.Role, role.ToString(), null, ClaimConstants.ApplicationIssuer));
		}

		return result;
	}

	private async Task<User> OnboardFirstUserAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default)
	{
		if ((await _userRepository.GetAllAsync(cancellationToken)).Any())
		{
			return null;
		}

		var user = new User();
		user.IdentityProviderExternalId = principal.FindFirst("oid").Value;
		user.Email = principal.FindFirst(x => x.Type == "upn")?.Value.Replace("@", "@devmail.");
		user.DisplayName = principal.FindFirst(x => x.Type == "name")?.Value;
		user.UserRoles.AddRange(Enum.GetValues<RoleEntry>().Select(entry => new UserRole() { RoleId = (int)entry }));

		_unitOfWork.AddForInsert(user);
		await _unitOfWork.CommitAsync(cancellationToken);

		_cacheService.Clear();

		return user;
	}
}
