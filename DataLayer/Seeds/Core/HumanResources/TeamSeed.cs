using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.HumanResources;
using Havit.Services.TimeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.HumanResources
{
	public class TeamSeed : DataSeed<CoreProfile>
	{
		private readonly ITimeService timeService;

		public TeamSeed(ITimeService timeService)
		{
			this.timeService = timeService;
		}

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
					Created = timeService.GetCurrentTime(),
				}
			};

			Seed(For(teams).PairBy(t => t.Id));
		}
	}
}
