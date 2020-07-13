using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Crm;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.DataLayer.Repositories.Security;
using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.HumanResources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.HumanResources
{
	[Service]
	public class G2EmployeeHistoryMigrator : IG2EmployeeHistoryMigrator
	{
		private readonly MigrationOptions options;
		private readonly IEmployeeHistoryRepository employeeHistoryRepository;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IEmploymentTermsRepository employmentTermsRepository;
		private readonly IEmploymentTypeRepository employmentTypeRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2EmployeeHistoryMigrator(
			IOptions<MigrationOptions> options,
			IEmployeeHistoryRepository employeeHistoryRepository,
			IEmployeeRepository employeeRepository,
			IEmploymentTermsRepository employmentTermsRepository,
			IEmploymentTypeRepository employmentTypeRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.employeeHistoryRepository = employeeHistoryRepository;
			this.employeeRepository = employeeRepository;
			this.employmentTermsRepository = employmentTermsRepository;
			this.employmentTypeRepository = employmentTypeRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateEmployeeHistories()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT hist.*, pos.Nazev JobPosition, et.Nazev EmploymentType FROM PracovnikHistorie hist LEFT JOIN PracovniPoziceLocalization pos ON hist.PracovniPoziceID = pos.PracovniPoziceID JOIN PracovniVztahLocalization et ON hist.PracovniVztahID = et.PracovniVztahID WHERE pos.LanguageID = 1 AND et.LanguageID = 1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var employeehistories = employeeHistoryRepository.GetAll();
			var employees = employeeRepository.GetAllIncludingDeleted();
			var employmentTypes = employmentTypeRepository.GetAll();
			var employmentTerms = employmentTermsRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var employee = employees.Find(e => e.MigrationId == reader.GetValue<int>("PracovnikID"));
				var startDate = reader.GetValue<DateTime>("DatumOd");
				var employeeHistoryID = reader.GetValue<int>("PracovnikHistorieID");

				Console.Write("PracovnikHistorie => EmployeeHistory: " + employeeHistoryID);
				var employeeHistory = employeehistories.Find(h => h.Employee == employee && h.StartDate == startDate);

				if (employeeHistory == null)
				{
					employeeHistory = new EmployeeHistory();
					unitOfWork.AddForInsert(employeeHistory);
					Console.WriteLine(" INSERT");
					employeeHistory.Employee = employee;
					employeeHistory.StartDate = startDate;
				}
				else
				{
					Console.WriteLine(" UPDATE");
					unitOfWork.AddForUpdate(employee);
				}

				employeeHistory.JobPosition = reader.GetValue<string>("jobPosition");
				employeeHistory.EmploymentType = employmentTypes.Find(t => t.Name == reader.GetValue<string>("employmentType"));
				employeeHistory.BasicRate = reader.GetValue<decimal>("SjednanaZakladniSazba");
				employeeHistory.EmploymentTerms = employmentTerms.Find(t => t.MigrationId == reader.GetValue<int>("TypUvazkuID"));
				employeeHistory.HourlyCost = reader.GetValue<decimal>("HodinovaSazbaOsobnichNakladu");
				employeeHistory.OverheadToPersonalCostsRatio = reader.GetValue<decimal?>("KoeficientPrirazkyRezie");
			}

			unitOfWork.Commit();
		}

	}
}
