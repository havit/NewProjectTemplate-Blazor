using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Seeds.Core;
using Havit.GoranG3.G2Migrator.Services.Finance;
using Havit.GoranG3.G2Migrator.Services.HumanResources;
using Havit.GoranG3.G2Migrator.Services.Projects;
using Havit.GoranG3.G2Migrator.Services.Sequences;
using Havit.GoranG3.G2Migrator.Services.Timesheets;
using Havit.GoranG3.G2Migrator.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace Havit.GoranG3.G2Migrator.Services
{
	[Service]
	public class G2MigrationRunner : IG2MigrationRunner
	{
		private readonly IG2UserMigrator userMigrator;
		private readonly IG2EmployeeMigrator employeeMigrator;
		private readonly IG2ProjectMigrator projectMigrator;
		private readonly IG2ProjectPhaseMigrator projectPhaseMigrator;
		private readonly IG2BankAccountMigrator bankAccountMigrator;
		private readonly IG2CurrencyMigrator currencyMigrator;
		private readonly IG2ExchangeRateMigrator exchangeRateMigrator;
		private readonly IG2TimesheetItemCategoryMigrator timesheetItemCategoryMigrator;
		private readonly IG2TimesheetItemMigrator timesheetItemMigrator;
		private readonly IG2OverheadToPersonalCostsRatioMigrator overheadToPersonalCostsRatioMigrator;
		private readonly IG2NumberSequenceMigrator numberSequenceMigrator;
		private readonly IG2NumberSequenceUnusedNumberMigrator numberSequenceUnusedNumberMigrator;
		private readonly IG2AbsenceTypeMigrator absenceTypeMigrator;
		private readonly IG2AbsenceMigrator absenceMigrator;
		private readonly IG2EmploymentTermsMigrator employmentTermsMigrator;
		private readonly IG2EmployeeHistoryMigrator employeeHistoryMigrator;
		private readonly IG2TeamMigrator teamMigrator;
		private readonly IDbContext dbContext;
		private readonly IDataSeedRunner dataSeedRunner;

		public G2MigrationRunner(
			IG2UserMigrator userMigrator,
			IG2EmployeeMigrator employeeMigrator,
			IG2ProjectMigrator projectMigrator,
			IG2ProjectPhaseMigrator projectPhaseMigrator,
			IG2BankAccountMigrator bankAccountMigrator,
			IG2CurrencyMigrator currencyMigrator,
			IG2ExchangeRateMigrator exchangeRateMigrator,
			IG2TimesheetItemCategoryMigrator timesheetItemCategoryMigrator,
			IG2TimesheetItemMigrator timesheetItemMigrator,
			IG2OverheadToPersonalCostsRatioMigrator overheadToPersonalCostsRatioMigrator,
			IG2NumberSequenceMigrator numberSequenceMigrator,
			IG2NumberSequenceUnusedNumberMigrator numberSequenceUnusedNumberMigrator,
			IG2AbsenceTypeMigrator absenceTypeMigrator,
			IG2AbsenceMigrator absenceMigrator,
			IG2EmploymentTermsMigrator employmentTermsMigrator,
			IG2EmployeeHistoryMigrator employeeHistoryMigrator,
			IG2TeamMigrator teamMigrator,
			IDbContext dbContext,
			IDataSeedRunner dataSeedRunner)
		{
			this.userMigrator = userMigrator;
			this.employeeMigrator = employeeMigrator;
			this.projectMigrator = projectMigrator;
			this.projectPhaseMigrator = projectPhaseMigrator;
			this.bankAccountMigrator = bankAccountMigrator;
			this.currencyMigrator = currencyMigrator;
			this.exchangeRateMigrator = exchangeRateMigrator;
			this.timesheetItemCategoryMigrator = timesheetItemCategoryMigrator;
			this.timesheetItemMigrator = timesheetItemMigrator;
			this.overheadToPersonalCostsRatioMigrator = overheadToPersonalCostsRatioMigrator;
			this.numberSequenceMigrator = numberSequenceMigrator;
			this.numberSequenceUnusedNumberMigrator = numberSequenceUnusedNumberMigrator;
			this.absenceTypeMigrator = absenceTypeMigrator;
			this.absenceMigrator = absenceMigrator;
			this.employmentTermsMigrator = employmentTermsMigrator;
			this.employeeHistoryMigrator = employeeHistoryMigrator;
			this.teamMigrator = teamMigrator;
			this.dbContext = dbContext;
			this.dataSeedRunner = dataSeedRunner;
		}

		public void Run()
		{
			dbContext.Database.Migrate();
			dataSeedRunner.SeedData<CoreProfile>();

			bankAccountMigrator.MigrateBankAccounts();
			currencyMigrator.MigrateCurrencies();
			//exchangeRateMigrator.MigrateExchangeRates();
			userMigrator.MigrateUsers();
			employeeMigrator.MigrateEmployees();
			projectMigrator.MigrateProjects();
			projectPhaseMigrator.MigrateProjectPhases();
			overheadToPersonalCostsRatioMigrator.MigrateOverheadToPersonalCostsRatios();
			timesheetItemCategoryMigrator.MigrateCategories();
			timesheetItemMigrator.MigrateTimesheetItems();
			numberSequenceUnusedNumberMigrator.MigrateUnusedNumbers();
			numberSequenceMigrator.MigrateSequences();
			absenceTypeMigrator.MigrateAbsenceTypes();
			absenceMigrator.MigrateAbsences();
			employmentTermsMigrator.MigrateEmploymentTerms();
			employeeHistoryMigrator.MigrateEmployeeHistories();
			teamMigrator.MigrateTeams();
		}
	}
}
