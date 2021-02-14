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
	public class G2ProjectMigrator : IG2ProjectMigrator
	{
		private readonly MigrationOptions options;
		private readonly IProjectRepository projectRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2ProjectMigrator(
			IOptions<MigrationOptions> options,
			IProjectRepository projectRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.projectRepository = projectRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateProjects()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM Projekt WHERE ProjektID <> -1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var projects = projectRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var projektID = reader.GetValue<int>("ProjektID");
				Console.Write($"PROJECT {projektID}: ");
				var project = projects.Find(p => p.MigrationId == projektID);
				Project parentProject = null;
				if (reader["ParentID"] != DBNull.Value)
				{
					parentProject = projects.Find(p => p.MigrationId == (int)reader["ParentID"]);
				}
				if (project == null)
				{
					Console.WriteLine(" INSERT");
					project = new Project(parentProject);
					project.MigrationId = projektID;
					projects.Add(project);
					unitOfWork.AddForInsert(project);
				}
				else
				{
					Console.WriteLine(" UPDATE");
					unitOfWork.AddForUpdate(project);
				}

				project.Parent = parentProject;
				project.ProjectCode = reader.GetValue<string>("Kod");
				project.Name = reader.GetValue<string>("Nazev");
				// TODO reader["Poznamka"] => AttridaObject.AttridaObjectComments.Add()
				// TODO project.ProjectManagerId
				project.PaymentDueDaysDefault = reader.GetValue<int?>("SplatnostPrijem");
				project.OverheadToPersonalCostsRatio = reader.GetValue<bool?>("UplatnovatRezijniPrirazkuOsobnichNakladu") == true ? (decimal?)null : 0m;
				project.IsActive = reader.GetValue<int?>("StavProjektuID") switch
				{
					1 => true,
					2 => true,
					null => null,
					_ => false
				};
				// TODO project.BusinessPartnerId
				project.Created = reader.GetValue<DateTime>("Created");
				project.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
