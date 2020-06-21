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
	public class G2EmployeeMigrator : IG2EmployeeMigrator
	{
		private readonly MigrationOptions options;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IUserRepository userRepository;
		private readonly ICountryByIsoCodeLookupService countryByIsoCodeLookupService;
		private readonly IUnitOfWork unitOfWork;

		public G2EmployeeMigrator(
			IOptions<MigrationOptions> options,
			IEmployeeRepository employeeRepository,
			IUserRepository userRepository,
			ICountryByIsoCodeLookupService countryByIsoCodeLookupService,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.employeeRepository = employeeRepository;
			this.userRepository = userRepository;
			this.countryByIsoCodeLookupService = countryByIsoCodeLookupService;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateEmployees()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM Pracovnik	LEFT JOIN Stat ON Pracovnik.AdresaDomuStatID = Stat.StatID", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var employees = employeeRepository.GetAllIncludingDeleted();
			var users = userRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var pracovnikID = reader.GetValue<int>("PracovnikID");
				string firstName = reader.GetValue<string>("KrestniJmeno");
				string lastName = reader.GetValue<string>("Prijmeni");
				Console.Write($"EMPLOYEE {pracovnikID}: ");
				var employee = employees.Find(p => p.MigrationId == pracovnikID);
				if (employee == null)
				{
					Console.WriteLine(" INSERT");
					employee = new Employee(firstName, lastName);
					employee.MigrationId = pracovnikID;
					employee.Contact = new Contact();
					employees.Add(employee);
					unitOfWork.AddForInsert(employee);
				}
				else
				{
					Console.WriteLine(" UPDATE");
					unitOfWork.AddForUpdate(employee);

					employee.FirstName = firstName;
					employee.LastName = lastName;
				}

				var userMigrationId = reader.GetValue<int?>("UzivatelID");
				if (userMigrationId != null)
				{
					employee.User = users.First(u => u.MigrationId == userMigrationId);
				}

				employee.TitlePrefix = reader.GetValue<string>("TitulyPredJmenem");
				employee.TitleSuffix = reader.GetValue<string>("TitulyPredJmenem");
				employee.BirthDate = reader.GetValue<DateTime?>("DatumNarozeni");

				employee.Contact.Name = employee.DisplayAs;
				employee.Contact.Email = reader.GetValue<string>("Email");
				employee.Contact.Phone = reader.GetValue<string>("Telefon");
				employee.Contact.Mobile = reader.GetValue<string>("Mobil");

				employee.Contact.RegisteredAddress ??= new Address();
				employee.Contact.RegisteredAddress.Line1 = reader.GetValue<string>("AdresaDomuUlice");
				employee.Contact.RegisteredAddress.City = reader.GetValue<string>("AdresaDomuMesto");
				employee.Contact.RegisteredAddress.Zip = reader.GetValue<string>("AdresaDomuPsc");

				var countryDomain = reader.GetValue<string>("TopLevelDomain");
				if (!String.IsNullOrWhiteSpace(countryDomain))
				{
					employee.Contact.RegisteredAddress.Country = countryByIsoCodeLookupService.GetCountryByIsoCode(countryDomain);
				}


				employee.Created = reader.GetValue<DateTime>("Created");
				employee.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}

	}
}
