using Havit.NewProjectTemplate.Model.Security;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Security;

public partial interface IUserRepository
{
	List<User> GetAllIncludingDeleted();
	Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
	Task<List<User>> GetUsersInRoleAsync(Role.Entry roleEntry, CancellationToken cancellationToken = default);
}
