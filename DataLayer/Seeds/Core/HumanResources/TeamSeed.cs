using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.HumanResources
{
	public class TeamSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var teams = new Team[]
			{
				new Team()
				{
					Id = (int)Team.Entry.Everyone,
					Name = "Everyone",
					IsActive = true,
					IsPrivateTeam = false,
					IsSystemTeam = true,
				}
			};

			Seed(For(teams));
		}
	}
}
