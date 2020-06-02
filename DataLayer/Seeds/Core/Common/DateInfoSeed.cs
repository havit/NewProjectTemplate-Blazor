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
					Id = 1,
					Date = new DateTime(2020, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Id = 2,
					Date = new DateTime(2020, 4, 10),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Id = 3,
					Date = new DateTime(2020, 4, 13),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Id = 4,
					Date = new DateTime(2020, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Id = 5,
					Date = new DateTime(2020, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Id = 6,
					Date = new DateTime(2020, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Id = 7,
					Date = new DateTime(2020, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Id = 8,
					Date = new DateTime(2020, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Id = 9,
					Date = new DateTime(2020, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Id = 10,
					Date = new DateTime(2020, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Id = 11,
					Date = new DateTime(2020, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Id = 12,
					Date = new DateTime(2020, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Id = 13,
					Date = new DateTime(2020, 12, 26),
					Description = "2. svátek vánoční"
				},
				new DateInfo()
				{
					Id = 14,
					Date = new DateTime(2021, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Id = 15,
					Date = new DateTime(2021, 4, 2),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Id = 16,
					Date = new DateTime(2021, 4, 5),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Id = 17,
					Date = new DateTime(2021, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Id = 18,
					Date = new DateTime(2021, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Id = 19,
					Date = new DateTime(2021, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Id = 20,
					Date = new DateTime(2021, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Id = 21,
					Date = new DateTime(2021, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Id = 22,
					Date = new DateTime(2021, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Id = 23,
					Date = new DateTime(2021, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Id = 24,
					Date = new DateTime(2021, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Id = 25,
					Date = new DateTime(2021, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Id = 26,
					Date = new DateTime(2021, 12, 26),
					Description = "2. svátek vánoční"
				},
				new DateInfo()
				{
					Id = 27,
					Date = new DateTime(2022, 1, 1),
					Description = "Nový rok"
				},
				new DateInfo()
				{
					Id = 28,
					Date = new DateTime(2022, 4, 15),
					Description = "Velký pátek"
				},
				new DateInfo()
				{
					Id = 29,
					Date = new DateTime(2022, 4, 18),
					Description = "Velikonoční pondělí"
				},
				new DateInfo()
				{
					Id = 30,
					Date = new DateTime(2022, 5, 1),
					Description = "Svátek práce"
				},
				new DateInfo()
				{
					Id = 31,
					Date = new DateTime(2022, 5, 8),
					Description = "Den osvobození od fašismu - 1945"
				},
				new DateInfo()
				{
					Id = 32,
					Date = new DateTime(2022, 7, 5),
					Description = "Den slovanských věrozvěstů Cyrila a Metoděje"
				},
				new DateInfo()
				{
					Id = 33,
					Date = new DateTime(2022, 7, 6),
					Description = "Den upálení mistra Jana Husa"
				},
				new DateInfo()
				{
					Id = 34,
					Date = new DateTime(2022, 9, 28),
					Description = "Den české státnosti"
				},
				new DateInfo()
				{
					Id = 35,
					Date = new DateTime(2022, 10, 28),
					Description = "Den vzniku samostatného Československého státu"
				},
				new DateInfo()
				{
					Id = 36,
					Date = new DateTime(2022, 11, 17),
					Description = "Den boje studentů za svobodu a demokracii - 1989"
				},
				new DateInfo()
				{
					Id = 37,
					Date = new DateTime(2022, 12, 24),
					Description = "Štědrý den"
				},
				new DateInfo()
				{
					Id = 38,
					Date = new DateTime(2022, 12, 25),
					Description = "1. svátek vánoční"
				},
				new DateInfo()
				{
					Id = 39,
					Date = new DateTime(2022, 12, 26),
					Description = "2. svátek vánoční"
				}
			};

			Seed(For(dateInfos).PairBy(dateInfo => dateInfo.Id));
		}
	}
}
