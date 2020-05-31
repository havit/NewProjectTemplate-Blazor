using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Seeds.Core;
using Havit.GoranG3.G2Migrator.Services.Timesheets;
using Microsoft.EntityFrameworkCore;

namespace Havit.GoranG3.G2Migrator.Services
{
	[Service]
	public class G2MigrationRunner : IG2MigrationRunner
	{
		private readonly IG2ProjectMigrator g2ProjectMigrator;
		private readonly IG2TimesheetItemCategoryMigrator g2TimesheetItemCategoryMigrator;
		private readonly IDbContext dbContext;
		private readonly IDataSeedRunner dataSeedRunner;

		public G2MigrationRunner(
			IG2ProjectMigrator g2ProjectMigrator,
			IG2TimesheetItemCategoryMigrator g2TimesheetItemCategoryMigrator,
			IDbContext dbContext,
			IDataSeedRunner dataSeedRunner)
		{
			this.g2ProjectMigrator = g2ProjectMigrator;
			this.g2TimesheetItemCategoryMigrator = g2TimesheetItemCategoryMigrator;
			this.dbContext = dbContext;
			this.dataSeedRunner = dataSeedRunner;
		}

		public void Run()
		{
			dbContext.Database.Migrate();
			dataSeedRunner.SeedData<CoreProfile>();

			g2TimesheetItemCategoryMigrator.MigrateCategories();
			//g2ProjectMigrator.MigrateProjects();
		}
	}
}
