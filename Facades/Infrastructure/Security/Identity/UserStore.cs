using Havit.Data.Patterns.UnitOfWorks;
using Havit.NewProjectTemplate.DataLayer.Repositories.Security;
using Havit.NewProjectTemplate.Model.Security;
using Microsoft.AspNetCore.Identity;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Identity;

public class UserStore :
	IUserStore<User>,
	IUserPasswordStore<User>,
	IUserEmailStore<User>,
	IUserSecurityStampStore<User>,
	IUserLockoutStore<User>,
	IUserRoleStore<User>
{
	private readonly IUserRepository userRepository;
	private readonly IRoleRepository roleRepository;
	private readonly IUnitOfWork unitOfWork;

	public UserStore(
		IUserRepository userRepository,
		IRoleRepository roleRepository,
		IUnitOfWork unitOfWork)
	{
		this.userRepository = userRepository;
		this.roleRepository = roleRepository;
		this.unitOfWork = unitOfWork;
	}

	public Task AddToRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedRoleName));

		var roleEntry = Enum.Parse<Role.Entry>(normalizedRoleName, ignoreCase: true);
		user.UserRoles.Add(new UserRole()
		{
			RoleId = (int)roleEntry,
			User = user,
			UserId = user.Id
		});

		return Task.CompletedTask;
	}

	public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		unitOfWork.AddForInsert(user);
		await unitOfWork.CommitAsync();
		return IdentityResult.Success;
	}

	public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		unitOfWork.AddForDelete(user);
		await unitOfWork.CommitAsync();
		return IdentityResult.Success;
	}

	public void Dispose()
	{
		// NOOP
	}

	public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedEmail));

		return await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
	}

	public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(userId));

		int id = int.Parse(userId);
		return await userRepository.GetObjectAsync(id);
	}

	public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedUserName));

		return await userRepository.GetByUsernameAsync(normalizedUserName);
	}

	public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.AccessFailedCount);
	}

	public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.Email);
	}

	public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.EmailConfirmed);
	}

	public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.LockoutEnabled);
	}

	public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.LockoutEnd);
	}

	public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.NormalizedEmail);
	}

	public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.NormalizedUsername);
	}

	public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.PasswordHash);
	}

	public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		// Role entity [Cache]d, no need for async
		return Task.FromResult<IList<string>>(user.UserRoles.Select(ur => roleRepository.GetObject(ur.RoleId).Name).ToList());
	}

	public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.SecurityStamp);
	}

	public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.Id.ToString());
	}

	public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.Username);
	}

	public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(roleName));

		var roleEntry = Enum.Parse<Role.Entry>(roleName, ignoreCase: true);
		return await userRepository.GetUsersInRoleAsync(roleEntry);
	}

	public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		return Task.FromResult(user.PasswordHash != null);
	}

	public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.AccessFailedCount++;
		return Task.FromResult(user.AccessFailedCount);
	}

	public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		var roleEntry = Enum.Parse<Role.Entry>(roleName, ignoreCase: true);
		return Task.FromResult(user.IsInRole(roleEntry));
	}

	public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		var roleEntry = Enum.Parse<Role.Entry>(roleName, ignoreCase: true);
		user.UserRoles.RemoveAll(ur => ur.RoleId == (int)roleEntry);

		return Task.CompletedTask;
	}

	public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.AccessFailedCount = 0;
		return Task.CompletedTask;
	}

	public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(email));

		user.Email = email;
		return Task.CompletedTask;
	}

	public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.EmailConfirmed = confirmed;
		return Task.CompletedTask;
	}

	public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.LockoutEnabled = enabled;
		return Task.CompletedTask;
	}

	public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.LockoutEnd = lockoutEnd;
		return Task.CompletedTask;
	}

	public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedEmail));

		user.NormalizedEmail = normalizedEmail;
		return Task.CompletedTask;
	}

	public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedName));

		user.NormalizedUsername = normalizedName;
		return Task.CompletedTask;
	}

	public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		user.PasswordHash = passwordHash;
		return Task.CompletedTask;
	}

	public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentNullException>(stamp != null);

		user.SecurityStamp = stamp;
		return Task.CompletedTask;
	}

	public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(userName));

		user.Username = userName;
		return Task.CompletedTask;
	}

	public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
	{
		Contract.Requires<ArgumentNullException>(user != null);

		unitOfWork.AddForUpdate(user);
		await unitOfWork.CommitAsync();
		return IdentityResult.Success;
	}
}
