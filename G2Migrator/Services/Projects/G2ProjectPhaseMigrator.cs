using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Projects;
using Havit.NewProjectTemplate.Model.Projects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Projects
{
	[Service]
	public class G2ProjectPhaseMigrator : IG2ProjectPhaseMigrator
	{
		private readonly MigrationOptions options;
		private readonly IProjectPhaseRepository projectPhaseRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2ProjectPhaseMigrator(
			IOptions<MigrationOptions> options,
			IProjectPhaseRepository projectPhaseRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.projectPhaseRepository = projectPhaseRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateProjectPhases()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT fa.*, loc.Nazev FROM Faze fa JOIN FazeLocalization loc ON loc.FazeID = fa.FazeID WHERE LanguageID = 1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var projectPhases = projectPhaseRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var projectPhaseID = reader.GetValue<int>("FazeID");
				Console.Write("Faze => ProjectPhase: " + projectPhaseID);
				var projectPhase = projectPhases.Find(p => p.MigrationId == projectPhaseID);

				if (projectPhase == null)
				{
					projectPhase = new ProjectPhase();
					projectPhase.MigrationId = projectPhaseID;
					unitOfWork.AddForInsert(projectPhase);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(projectPhase);
					Console.WriteLine(" UPDATE");
				}

				projectPhase.Name = reader.GetValue<string>("Nazev");
				projectPhase.Code = reader.GetValue<string>("Kod");
				projectPhase.UiOrder = reader.GetValue<int>("Poradi");
				projectPhase.Created = reader.GetValue<DateTime>("Created");
				projectPhase.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
