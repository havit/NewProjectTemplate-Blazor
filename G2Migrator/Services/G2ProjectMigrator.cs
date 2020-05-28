using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Projects;
using Havit.GoranG3.Model.Projects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services
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

			var projects = projectRepository.GetAll();
			
			while (reader.Read())
			{
				Console.WriteLine(reader["ProjektID"]);
				var projektID = (int)reader["ProjektID"];
				var project = projects.Find(p => p.MigrationId == projektID);
				if (project == null)
				{
					project = new Project();
					project.MigrationId = projektID;
					unitOfWork.AddForInsert(project);
				}
				else
				{
					unitOfWork.AddForUpdate(project);
				}

				if (reader["ParentID"] != DBNull.Value)
				{
					project.Parent = projects.Find(p => p.MigrationId == (int)reader["ParentID"]);
				}
				project.ProjectCode = (string)reader["Kod"];
				project.Name = (string)reader["Nazev"];
				// TODO reader["Poznamka"] => AttridaObject.AttridaObjectComments.Add()
				// TODO project.ProjectManagerId
				project.PaymentDueDaysDefault = (int)reader["SplatnostPrijem"];
				project.OverheadToPersonalCostsRatio = (bool)reader["UplatnovatRezijniPrirazkuOsobnichNakladu"] ? (decimal?)null : 0m;
				project.IsActive = (int?)reader["StavProjektuID"] switch
				{
					1 => true,
					2 => true,
					null => null,
					_ => false
				};
				// TODO project.BusinessPartnerId
				project.Created = (DateTime)reader["Created"];
				project.Deleted = (DateTime)reader["Deleted"];
			}

			unitOfWork.Commit();
		}
	}
}
