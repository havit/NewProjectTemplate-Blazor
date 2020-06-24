using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataLoaders;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.DataLayer.Repositories.Projects;
using Havit.GoranG3.DataLayer.Repositories.Timesheets;
using Havit.GoranG3.Model.HumanResources;
using Havit.GoranG3.Model.Projects;
using Havit.GoranG3.Model.Security;
using Havit.GoranG3.Model.Timesheets;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.Timesheets
{
	[Service]
	public class G2TimesheetItemMigrator : IG2TimesheetItemMigrator
	{
		private readonly MigrationOptions options;
		private readonly ITimesheetItemRepository timesheetItemRepository;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IProjectRepository projectRepository;
		private readonly IProjectPhaseRepository projectPhaseRepository;
		private readonly ITimesheetItemCategoryRepository timesheetItemCategoryRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IDataLoader dataLoader;

		public G2TimesheetItemMigrator(
			IOptions<MigrationOptions> options,
			ITimesheetItemRepository timesheetItemRepository,
			IEmployeeRepository employeeRepository,
			IProjectRepository projectRepository,
			IProjectPhaseRepository projectPhaseRepository,
			ITimesheetItemCategoryRepository timesheetItemCategoryRepository,
			IUnitOfWork unitOfWork,
			IDataLoader dataLoader)
		{
			this.options = options.Value;
			this.timesheetItemRepository = timesheetItemRepository;
			this.employeeRepository = employeeRepository;
			this.projectRepository = projectRepository;
			this.projectPhaseRepository = projectPhaseRepository;
			this.timesheetItemCategoryRepository = timesheetItemCategoryRepository;
			this.unitOfWork = unitOfWork;
			this.dataLoader = dataLoader;
		}
		public void MigrateTimesheetItems()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM TimesheetItem", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var timesheetItems = timesheetItemRepository.GetAllIncludingDeleted();
			var employees = employeeRepository.GetAllIncludingDeleted();
			var projects = projectRepository.GetAllIncludingDeleted().ToDictionary(p => p.MigrationId);
			var projectPhases = projectPhaseRepository.GetAllIncludingDeleted().ToDictionary(p => p.MigrationId);
			var categories = timesheetItemCategoryRepository.GetAllIncludingDeleted().ToDictionary(c => c.MigrationId);
			dataLoader.LoadAll(employees, e => e.User);

			var migrationIds = timesheetItems.Select(item => item.MigrationId).ToHashSet();

			while (reader.Read())
			{
				var timesheetItemID = reader.GetValue<int>("TimesheetItemID");
				Console.Write("Timesheet item " + timesheetItemID);
				if (!migrationIds.Contains(timesheetItemID))
				{
					TimesheetItem timesheetItem = new TimesheetItem();
					timesheetItem.MigrationId = timesheetItemID;
					Console.WriteLine(" INSERT");

					timesheetItem.Employee = employees.Find(e => e.MigrationId == (int)reader["PracovnikID"]);
					timesheetItem.Date = reader.GetValue<DateTime>("Datum");

					Project project = null;
					projects.TryGetValue((int)reader["ProjektID"], out project);
					timesheetItem.Project = project;

					ProjectPhase projectPhase = null;
					if (reader["FazeID"] != DBNull.Value)
					{
						projectPhases.TryGetValue((int)reader["FazeID"], out projectPhase);
						timesheetItem.ProjectPhase = projectPhase;
					}

					timesheetItem.DurationHours = reader.GetValue<decimal>("PocetHodin");
					timesheetItem.PersonalCosts = reader.GetValue<decimal?>("OsobniNaklady");
					timesheetItem.OverheadCosts = reader.GetValue<decimal?>("RezijniPrirazkaVuciOsobnimNakladum");

					TimesheetItemCategory category = null;
					if (reader["TimesheetItemCategoryID"] != DBNull.Value)
					{
						categories.TryGetValue((int)reader["TimesheetItemCategoryID"], out category);
						timesheetItem.TimesheetItemCategory = category;
					}

					timesheetItem.Text = reader.GetValue<string>("Text");

					Employee employee = null;
					if (reader["SchvalilID"] != DBNull.Value)
					{
						employee = employees.Find(e => e.MigrationId == (int)reader["SchvalilID"]);
						timesheetItem.ApprovedBy = employee.User;
					}

					timesheetItem.ApprovedAt = reader.GetValue<DateTime?>("SchvalenoKdy");
					timesheetItem.ExternalId = reader.GetValue<int?>("ExternalWorkItemID");
					timesheetItem.ExternalUpdatePending = reader.GetValue<bool>("TfsUpdatePending");
					timesheetItem.Created = reader.GetValue<DateTime>("Created");
					timesheetItem.Deleted = reader.GetValue<DateTime?>("Deleted");
					unitOfWork.AddForInsert(timesheetItem);
				}
				else
				{
					Console.WriteLine(" already exists. Skipping item.");
				}
			}

			unitOfWork.Commit();
		}
	}
}
