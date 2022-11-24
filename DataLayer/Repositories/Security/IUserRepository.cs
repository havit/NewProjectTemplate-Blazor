using Havit.NewProjectTemplate.Model.Security;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Security;

public partial interface IUserRepository
{
	Task<User> GetByIdentityProviderIdAsync(string identityProviderId, CancellationToken cancellationToken = default);
}
