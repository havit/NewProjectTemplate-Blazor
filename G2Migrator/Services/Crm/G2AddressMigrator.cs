using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Crm;
using Havit.NewProjectTemplate.Model.Crm;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Crm
{
	[Service]
	public class G2AddressMigrator : IG2AddressMigrator
	{
		private readonly MigrationOptions options;
		private readonly IAddressRepository addressRepository;
		private readonly ICountryRepository countryRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2AddressMigrator(
			IOptions<MigrationOptions> options,
			IAddressRepository addressRepository,
			ICountryRepository countryRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.addressRepository = addressRepository;
			this.countryRepository = countryRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateAddresses()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT addr.*, coun.DicPredpona FROM AdresaSubjektu addr LEFT JOIN Stat coun ON addr.StatID = coun.StatID", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var addresses = addressRepository.GetAll();
			var countries = countryRepository.GetAll();

			while (reader.Read())
			{
				var addressID = reader.GetValue<int>("AdresaSubjektuID");
				Console.Write("AdresaSubjektu => Address: " + addressID);
				var address = addresses.Find(a => a.MigrationId == addressID);

				if (address == null)
				{
					address = new Address();
					address.MigrationId = addressID;
					unitOfWork.AddForInsert(address);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(address);
					Console.WriteLine(" UPDATE");
				}

				address.Line1 = reader.GetValue<string>("Nazev");
				address.Line2 = reader.GetValue<string>("Ulice");
				address.City = reader.GetValue<string>("Obec");
				address.Zip = reader.GetValue<string>("Psc");

				if (reader["DicPredpona"] != DBNull.Value)
				{
					var country = countries.Find(c => c.IsoCode == (string)reader["DicPredpona"]);
					address.Country = country;
				}
			}

			unitOfWork.Commit();
		}
	}
}
