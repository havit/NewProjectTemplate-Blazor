using System.Security;
using System.Security.Claims;
using System.Threading;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure.Security;
using Havit.NewProjectTemplate.DataLayer.Repositories.Security;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Model.Security;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Services.Caching;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

[Service(Profile = ServiceProfiles.WebServer)]
public class CustomClaimsBuilder : ICustomClaimsBuilder
{
	private readonly IUserRepository _userRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICacheService _cacheService;
	private readonly IUserContextInfoBuilder _userContextInfoBuilder;

	public CustomClaimsBuilder(
		IUserContextInfoBuilder userContextInfoBuilder,
		IUserRepository userRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService)
	{
		_userContextInfoBuilder = userContextInfoBuilder;
		_userRepository = userRepository;
		_unitOfWork = unitOfWork;
		_cacheService = cacheService;
	}

	public async Task<List<Claim>> GetCustomClaimsAsync(ClaimsPrincipal principal)
	{
		Contract.Requires<SecurityException>(principal.Identity.IsAuthenticated);

		List<Claim> result = new();

		UserContextInfo userContextInfo = _userContextInfoBuilder.GetUserContextInfo(principal);

		var user = await _userRepository.GetByIdentityProviderIdAsync(userContextInfo.IdentityProviderExternalId);

		if (user == null)
		{
#if DEBUG
			user = await OnboardFirstUserAsync(userContextInfo, principal);
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

	private async Task<User> OnboardFirstUserAsync(UserContextInfo userContextInfo, ClaimsPrincipal principal, CancellationToken cancellationToken = default)
	{
		if ((await _userRepository.GetAllAsync(cancellationToken)).Any())
		{
			return null;
		}

		var user = new User();
		user.IdentityProviderExternalId = userContextInfo.IdentityProviderExternalId;
		user.Email = principal.FindFirst(x => x.Type == "upn")?.Value.Replace("@", "@devmail.");
		user.DisplayName = principal.FindFirst(x => x.Type == "name")?.Value;
		user.UserRoles.AddRange(Enum.GetValues<RoleEntry>().Select(entry => new UserRole() { RoleId = (int)entry }));

		_unitOfWork.AddForInsert(user);
		await _unitOfWork.CommitAsync(cancellationToken);

		_cacheService.Clear();

		return user;
	}
}
