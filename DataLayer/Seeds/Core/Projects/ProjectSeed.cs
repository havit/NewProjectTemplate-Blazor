using Havit.Data.Patterns.DataSeeds;
using Havit.Data.Patterns.Exceptions;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.DataLayer.Repositories.Projects;
using Havit.GoranG3.Model.Projects;
using Havit.Services.TimeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.Projects
{
	public class ProjectSeed : DataSeed<CoreProfile>
	{
		private readonly IProjectRepository projectRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly ITimeService timeService;

		public ProjectSeed(
			IProjectRepository projectRepository,
			IUnitOfWork unitOfWork,
			ITimeService timeService)
		{
			this.projectRepository = projectRepository;
			this.unitOfWork = unitOfWork;
			this.timeService = timeService;
		}

		public override void SeedData()
		{
			try
			{
				var actual = projectRepository.GetObject((int)Project.Entry.Root);
			}
			catch (ObjectNotFoundException)
			{
				var root = Project.CreateRootProject();
				root.IsActive = true;
				root.Name = "ROOT_SYSTEM_PROJECT";
				root.ProjectCode = "ROOT";
				root.MigrationId = -1;
				root.Created = timeService.GetCurrentTime();

				unitOfWork.AddForInsert(root);
				unitOfWork.Commit();
			}

			//Seed(For(projects).PairBy(p => p.Id).WithoutUpdate()); // TODO Seed s private/protected settery
		}
	}
}
