using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.Model.HumanResources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.HumanResources
{
	[Service]
	public class G2EmploymentTermsMigrator : IG2EmploymentTermsMigrator
	{
		private readonly MigrationOptions options;
		private readonly IEmploymentTermsRepository employmentTermsRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2EmploymentTermsMigrator(
			IOptions<MigrationOptions> options,
			IEmploymentTermsRepository employmentTermsRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.employmentTermsRepository = employmentTermsRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateEmploymentTerms()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT terms.*, loc.Nazev FROM TypUvazku terms JOIN TypUvazkuLocalization loc ON loc.TypUvazkuID = terms.TypUvazkuID WHERE LanguageID = 1 AND Nazev NOT LIKE '%přesčasy propláceny%'", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var employmentTerms = employmentTermsRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var employmentTermID = reader.GetValue<int>("TypUvazkuID");
				var rateType = (EmployeeRateType)reader.GetValue<int>("TypOdmenovani");
				var hoursPerDay = reader.GetValue<decimal>("DenniFondPracovniDoby");

				Console.Write("TypUvazku => EmploymentTerms: " + employmentTermID);
				var employmentTerm = employmentTerms.Find(p => p.RateType == rateType && p.HoursPerDay == hoursPerDay);

				if (employmentTerm == null)
				{
					employmentTerm = new EmploymentTerms();
					unitOfWork.AddForInsert(employmentTerm);
					Console.WriteLine(" INSERT");
					employmentTerm.MigrationId = employmentTermID;
				}
				else
				{
					unitOfWork.AddForUpdate(employmentTerm);
					Console.WriteLine(" UPDATE");
				}

				employmentTerm.Name = reader.GetValue<string>("Nazev");
				employmentTerm.RateType = rateType;
				employmentTerm.HoursPerDay = hoursPerDay;
			}

			unitOfWork.Commit();
		}
	}
}
