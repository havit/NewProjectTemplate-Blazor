using Havit.Diagnostics.Contracts;
using Havit.GoranG3.DataLayer.Repositories.Security;
using Havit.GoranG3.Model.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Identity
{
	public class RoleStore : IRoleStore<Role>
	{
		private readonly IRoleRepository roleRepository;

		public RoleStore(IRoleRepository roleRepository)
		{
			this.roleRepository = roleRepository;
		}

		public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
			// NOOP
		}

		public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(roleId), nameof(roleId));
			cancellationToken.ThrowIfCancellationRequested();

			var id = int.Parse(roleId);
			return Task.FromResult(roleRepository.GetObject(id)); // role is [Cache]d, no need for async
		}

		public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(normalizedRoleName), nameof(normalizedRoleName));
			cancellationToken.ThrowIfCancellationRequested();

			var roleEntry = Enum.Parse<Role.Entry>(normalizedRoleName, ignoreCase: true);
			return Task.FromResult(roleRepository.GetObject((int)roleEntry)); // role is [Cache]d, no need for async
		}

		public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(role != null, nameof(role));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(role.NormalizedName);
		}

		public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(role != null, nameof(role));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(role.Id.ToString());
		}

		public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
		{
			Contract.Requires<ArgumentNullException>(role != null, nameof(role));
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(role.Name);
		}

		public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}
	}
}
