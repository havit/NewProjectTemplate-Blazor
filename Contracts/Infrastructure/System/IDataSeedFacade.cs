using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.Infrastructure.System;

[ApiContract]
public interface IDataSeedFacade
{
	Task SeedDataProfileAsync(string profileName, CancellationToken cancellationToken = default);

	Task<List<string>> GetDataSeedProfilesAsync(CancellationToken cancellationToken = default);
}
