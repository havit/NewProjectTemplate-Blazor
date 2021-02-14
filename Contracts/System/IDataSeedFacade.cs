using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.System
{
	[ServiceContract]
	public interface IDataSeedFacade
	{
		Task SeedDataProfile(string profileName);

		Task<Dto<string[]>> GetDataSeedProfiles();
	}
}