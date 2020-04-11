using System.Collections.Generic;

namespace Havit.GoranG3.Facades.System
{
    public interface IDataSeedFacade
    {
		void SeedDataProfile(string profileName);

        IList<string> GetDataSeedProfiles();
    }
}