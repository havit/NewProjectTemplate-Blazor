using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.Infrastructure;

[ApiContract]
public interface IDataSeedFacade
{
	Task SeedDataProfileAsync(Dto<string> profileName, CancellationToken cancellationToken = default);

	Task<List<string>> GetDataSeedProfilesAsync(CancellationToken cancellationToken = default);
}
