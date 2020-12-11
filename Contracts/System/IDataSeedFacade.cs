using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.System
{
	public interface IDataSeedFacade
	{
		Task SeedDataProfile(string profileName);

		Task<Dto<string[]>> GetDataSeedProfiles();
	}
}