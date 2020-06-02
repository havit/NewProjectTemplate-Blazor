using System;
using System.Collections.Generic;
using System.Text;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Common;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Common
{
	public class DateInfoSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var dateInfos = new[] 
			{
				new DateInfo()
				{
					Date = new DateTime(2020, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 4, 10),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 4, 13),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Date = new DateTime(2020, 12, 26),
					Description = "2. svátek vánoční"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 4, 2),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 4, 5),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Date = new DateTime(2021, 12, 26),
					Description = "2. svátek vánoční"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 4, 15),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 4, 18),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Date = new DateTime(2022, 12, 26),
					Description = "2. svátek vánoční"
				}
			};

			Seed(For(dateInfos).PairBy(dateInfo => dateInfo.Date));
		}
	}
}
