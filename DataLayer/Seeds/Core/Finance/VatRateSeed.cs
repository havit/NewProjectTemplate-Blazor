using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Finance
{
	public class VatRateSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var id = -1;
			var rates = new[]
			{
				new VatRate()
				{
					Id = id--,
					Rate = 0.22m
				},
				new VatRate()
				{
					Id = id--,
					Rate = 0.15m
				},
				new VatRate()
				{
					Id = id--,
					Rate = 0.10m
				},
				new VatRate()
				{
					Id = id--,
					Rate = 0
				}
			};

			Seed(For(rates).PairBy(r => r.Id)); // TODO FUTURE Removal of excessive items
		}
	}
}
