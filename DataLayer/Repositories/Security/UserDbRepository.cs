using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataLoaders;
using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Model.Security;
using Microsoft.EntityFrameworkCore;

namespace Havit.GoranG3.DataLayer.Repositories.Security
{
	public partial class UserDbRepository : IUserRepository
	{
		protected override IEnumerable<Expression<Func<User, object>>> GetLoadReferences()
		{
			yield return (User u) => u.UserRoles;
		}

		public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(username), nameof(username));
			cancellationToken.ThrowIfCancellationRequested();

			var normalizedUsername = username.ToUpper();
			return await Data.FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUsername, cancellationToken);
		}

		public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(email), nameof(email));
			cancellationToken.ThrowIfCancellationRequested();

			var normalizedEmail = email.ToUpper();
			return await Data.FirstOrDefaultAsync(u => u.NormalizedEmail == email);
		}

		public async Task<List<User>> GetUsersInRoleAsync(Role.Entry roleEntry, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await Data.Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)roleEntry)).ToListAsync();
		}
	}
}