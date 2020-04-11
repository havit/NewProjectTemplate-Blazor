using Havit.GoranG3.Model.Common;
using Havit.Data.Patterns.DataSeeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Common
{
    public class ApplicationSettingsSeed : DataSeed<CoreProfile>
    {
        public override void SeedData()
        {
            ApplicationSettings settings = new ApplicationSettings
            {
                Id = (int)ApplicationSettings.Entry.Current,
				// TODO: Výchozí nastavení
            };

            Seed(For(settings).PairBy(item => item.Id).WithoutUpdate());
        }
    }
}
