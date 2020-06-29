using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Timesheets;
using Havit.GoranG3.Model.Timesheets;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.Timesheets
{
	[Service]
	public class G2TimesheetItemCategoryMigrator : IG2TimesheetItemCategoryMigrator
	{
		private readonly MigrationOptions options;
		private readonly ITimesheetItemCategoryRepository timesheetItemCategoryRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2TimesheetItemCategoryMigrator(
			IOptions<MigrationOptions> options,
			ITimesheetItemCategoryRepository timesheetItemCategoryRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.timesheetItemCategoryRepository = timesheetItemCategoryRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateCategories()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT ti.*, loc.Nazev FROM TimesheetItemCategory ti INNER JOIN TimesheetItemCategoryLocalization loc ON loc.TimesheetItemCategoryID = ti.TimesheetItemCategoryID WHERE LanguageID = 1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var categories = timesheetItemCategoryRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var categoryID = reader.GetValue<int>("TimesheetItemCategoryID");
				Console.Write("TimesheetItemCategory :" + categoryID);
				var category = categories.Find(p => p.MigrationId == categoryID);
				if (category == null)
				{
					category = new TimesheetItemCategory();
					category.MigrationId = categoryID;
					unitOfWork.AddForInsert(category);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(category);
					Console.WriteLine(" UPDATE");
				}

				category.Name = reader.GetValue<string>("Nazev");
				category.Created = reader.GetValue<DateTime>("Created");
				category.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
