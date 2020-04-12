using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(username), nameof(username));
			cancellationToken.ThrowIfCancellationRequested();

			return await Data.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
		}

		public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(email), nameof(email));
			cancellationToken.ThrowIfCancellationRequested();

			return await Data.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}