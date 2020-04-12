using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Diagnostics.Contracts;
using Havit.GoranG3.DataLayer.Repositories.Security;
using Havit.GoranG3.Model.Security;
using Microsoft.AspNetCore.Identity;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Identity
{
	public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
	{
		private readonly IUserRepository userRepository;
		private readonly IUnitOfWork unitOfWork;

		public UserStore(
			IUserRepository userRepository,
			IUnitOfWork unitOfWork)
		{
			this.userRepository = userRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			unitOfWork.AddForInsert(user);
			await unitOfWork.CommitAsync();
			return IdentityResult.Success;
		}

		public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

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
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedEmail), nameof(normalizedEmail));
			cancellationToken.ThrowIfCancellationRequested();

			return await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
		}

		public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(userId), nameof(userId));
			cancellationToken.ThrowIfCancellationRequested();

			return await userRepository.GetObjectAsync(int.Parse(userId));
		}

		public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedUserName), nameof(normalizedUserName));
			cancellationToken.ThrowIfCancellationRequested();

			return await userRepository.GetByUsernameAsync(normalizedUserName);
		}

		public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.EmailConfirmed);
		}

		public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.Email.ToLowerInvariant());
		}

		public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.Username.ToLowerInvariant());
		}

		public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.PasswordHash);
		}

		public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.Id.ToString());
		}

		public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.Username);
		}

		public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(user.PasswordHash != null);
		}

		public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(email), nameof(email));
			cancellationToken.ThrowIfCancellationRequested();

			user.Email = email;
			return Task.CompletedTask;
		}

		public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			user.EmailConfirmed = confirmed;
			return Task.CompletedTask;
		}

		public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedEmail), nameof(normalizedEmail));
			cancellationToken.ThrowIfCancellationRequested();

			user.Email = normalizedEmail;
			return Task.CompletedTask;
		}

		public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedName), nameof(normalizedName));
			cancellationToken.ThrowIfCancellationRequested();

			user.Username = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			user.PasswordHash = passwordHash;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(userName), nameof(userName));
			cancellationToken.ThrowIfCancellationRequested();

			user.Username = userName;
			return Task.CompletedTask;
		}

		public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(user != null, nameof(user));
			cancellationToken.ThrowIfCancellationRequested();

			unitOfWork.AddForUpdate(user);
			await unitOfWork.CommitAsync();
			return IdentityResult.Success;
		}
	}
}
