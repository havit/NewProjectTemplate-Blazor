using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.HumanResources;
using Havit.NewProjectTemplate.Model.HumanResources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.HumanResources
{
	[Service]
	public class G2AbsenceMigrator : IG2AbsenceMigrator
	{
		private readonly MigrationOptions options;
		private readonly IAbsenceRepository absenceRepository;
		private readonly IAbsenceTypeRepository absenceTypeRepository;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2AbsenceMigrator(
			IOptions<MigrationOptions> options,
			IAbsenceRepository absenceRepository,
			IAbsenceTypeRepository absenceTypeRepository,
			IEmployeeRepository employeeRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.absenceRepository = absenceRepository;
			this.absenceTypeRepository = absenceTypeRepository;
			this.employeeRepository = employeeRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateAbsences()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM PracovniVolno", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var absences = absenceRepository.GetAllIncludingDeleted();
			var absenceTypes = absenceTypeRepository.GetAllIncludingDeleted();
			var employees = employeeRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var absenceID = reader.GetValue<int>("PracovniVolnoID");
				Console.Write("PracovniVolno => Absence: " + absenceID);
				var absence = absences.Find(p => p.MigrationId == absenceID);

				if (absence == null)
				{
					absence = new Absence();
					absence.MigrationId = absenceID;
					unitOfWork.AddForInsert(absence);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(absence);
					Console.WriteLine(" UPDATE");
				}

				absence.Employee = employees.Find(e => e.MigrationId == (int)reader["PracovnikID"]);
				absence.StartDate = reader.GetValue<DateTime>("Datum");
				absence.EndDate = reader.GetValue<DateTime?>("DatumDo");
				absence.Days = reader.GetValue<decimal>("PohybDnu");
				absence.AbsenceType = absenceTypes.Find(a => a.MigrationId == (int)reader["TypPracovnihoVolnaID"]);
				absence.Description = reader.GetValue<string>("Popis");

				Employee employee = null;
				if (reader["SchvalilID"] != DBNull.Value)
				{
					employee = employees.Find(e => e.MigrationId == (int)reader["SchvalilID"]);
					absence.ApprovedBy = employee.User;
				}

				absence.ApprovedAt = reader.GetValue<DateTime?>("SchvalenoKdy");
				absence.Created = reader.GetValue<DateTime>("Created");
				absence.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
