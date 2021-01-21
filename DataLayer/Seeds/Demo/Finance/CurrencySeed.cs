using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.DataLayer.Seeds.Demo.Finance
{
	public class CurrencySeed : DataSeed<DemoProfile>
	{
		public override void SeedData()
		{
			var currencies = new[]
			{
				new Currency() { Code = "CZK" },
				new Currency() { Code = "EUR" },
				new Currency() { Code = "USD" }
			};

			Seed(For(currencies).PairBy(c => c.Code));
		}
	}
}
