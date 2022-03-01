using System.Linq.Expressions;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.NewProjectTemplate.Model.Security;
using Microsoft.EntityFrameworkCore;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Security;

public partial class UserDbRepository : IUserRepository
{
	public List<User> GetAllIncludingDeleted()
	{
		return DataIncludingDeleted.Include(GetLoadReferences).ToList();
	}

	public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(username));

		var normalizedUsername = username.ToUpper();
		return await Data.Include(GetLoadReferences).FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUsername, cancellationToken);
	}

	public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(email));

		var normalizedEmail = email.ToUpper();
		return await Data.Include(GetLoadReferences).FirstOrDefaultAsync(u => u.NormalizedEmail == email);
	}

	public async Task<List<User>> GetUsersInRoleAsync(Role.Entry roleEntry, CancellationToken cancellationToken = default)
	{
		return await Data.Include(GetLoadReferences).Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)roleEntry)).ToListAsync();
	}

	protected override IEnumerable<Expression<Func<User, object>>> GetLoadReferences()
	{
		yield return (User u) => u.UserRoles;
	}
}
