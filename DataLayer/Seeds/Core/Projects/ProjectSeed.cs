using Havit.Data.Patterns.DataSeeds;
using Havit.Data.Patterns.Exceptions;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.DataLayer.Repositories.Projects;
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
		private readonly IProjectRepository projectRepository;
		private readonly IUnitOfWork unitOfWork;

		public ProjectSeed(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
		{
			this.projectRepository = projectRepository;
			this.unitOfWork = unitOfWork;
		}


		public override void SeedData()
		{
			try
			{
				var actual = projectRepository.GetObject((int)Project.Entry.Root);
			}
			catch (ObjectNotFoundException)
			{
				var root = new Project()
				{
					Id = (int)Project.Entry.Root,
					IsActive = true,
					Name = "ROOT_SYSTEM_PROJECT",
					ProjectCode = "ROOT",
					MigrationId = -1
				};

				unitOfWork.AddForInsert(root);
				unitOfWork.Commit();
			}

			//Seed(For(projects).PairBy(p => p.Id).WithoutUpdate()); // TODO Seed s private/protected settery
		}
	}
}
