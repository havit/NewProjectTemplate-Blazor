using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.System
{
	[ApiContract]
	public interface IDataSeedFacade
	{
		Task SeedDataProfileAsync(string profileName, CancellationToken cancellationToken = default);

		Task<Dto<string[]>> GetDataSeedProfilesAsync(CancellationToken cancellationToken = default);
	}
}