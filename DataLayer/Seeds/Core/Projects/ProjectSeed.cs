using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Projects
{
	public class ProjectSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var projects = new[]
			{
				new Project()
				{
					Id = (int)Project.Entry.Root,
					IsActive = true,
					Name = "ROOT_SYSTEM_PROJECT",
					ProjectCode = "ROOT",
					MigrationId = -1
				}
			};

			//Seed(For(projects).PairBy(p => p.Id).WithoutUpdate()); // TODO Seed s private/protected settery
		}
	}
}
