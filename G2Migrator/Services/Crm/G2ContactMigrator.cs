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
	public class G2ContactMigrator : IG2ContactMigrator
	{
		private readonly MigrationOptions options;
		private readonly IAddressRepository addressRepository;
		private readonly IContactRepository contactRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2ContactMigrator(
			IOptions<MigrationOptions> options,
			IAddressRepository addressRepository,
			IContactRepository contactRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.addressRepository = addressRepository;
			this.contactRepository = contactRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateContacts()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT *, (SELECT Hodnota FROM SpojeniSubjektu WHERE Subjekt.SubjektID = SpojeniSubjektu.SubjektID AND TypSpojeniSubjektuID = -1 AND IsHlavniSpojeniSubjektu = 1) AS Phone, (SELECT Hodnota FROM SpojeniSubjektu WHERE Subjekt.SubjektID = SpojeniSubjektu.SubjektID AND TypSpojeniSubjektuID = -2 AND IsHlavniSpojeniSubjektu = 1) AS Mobile, (SELECT Hodnota FROM SpojeniSubjektu WHERE Subjekt.SubjektID = SpojeniSubjektu.SubjektID AND TypSpojeniSubjektuID = -4 AND IsHlavniSpojeniSubjektu = 1) AS Email, (SELECT Hodnota FROM SpojeniSubjektu WHERE Subjekt.SubjektID = SpojeniSubjektu.SubjektID AND TypSpojeniSubjektuID = -5 AND IsHlavniSpojeniSubjektu = 1) AS Web FROM Subjekt", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var addresses = addressRepository.GetAll();
			var contacts = contactRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var contactID = reader.GetValue<int>("SubjektID");
				Console.Write("Subjekt => Contact: " + contactID);
				var contact = contacts.Find(c => c.MigrationId == contactID);

				if (contact == null)
				{
					contact = new Contact();
					contact.MigrationId = contactID;
					unitOfWork.AddForInsert(contact);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(contact);
					Console.WriteLine(" UPDATE");
				}

				contact.Name = reader.GetValue<string>("Jmeno");

				if (reader["HlavniAdresaID"] != DBNull.Value)
				{
					contact.RegisteredAddress = addresses.Find(a => a.MigrationId == (int)reader["HlavniAdresaID"]);
				}

				contact.Phone = reader.GetValue<string>("Phone");
				contact.Mobile = reader.GetValue<string>("Mobile");
				contact.Email = reader.GetValue<string>("Email");
				contact.Web = reader.GetValue<string>("Web");

				contact.CompanyRegistrationNumber = reader.GetValue<string>("Ico");
				contact.TaxRegistrationNumber = reader.GetValue<string>("Dic");
				contact.IsVatPayer = reader.GetValue<bool>("PlatceDph");
				contact.CertificateOfIncorporation = reader.GetValue<string>("ZapisDoRejstriku");
				contact.BankName = reader.GetValue<string>("BankovniSpojeni");

				var bankAccount = reader.GetValue<string>("CisloUctuZaklad");
				if (!String.IsNullOrWhiteSpace(bankAccount))
				{
					contact.BankAccountNumber = String.Concat(bankAccount, "/", reader.GetValue<string>("CisloUctuKodBanky"));
				}

				contact.BankAccountIban = reader.GetValue<string>("CisloUctuIban");
				contact.BankAccountSwiftBic = reader.GetValue<string>("CisloUctuSwiftBic");
				contact.Note = reader.GetValue<string>("Poznamky");
				contact.IsArchived = reader.GetValue<bool>("IsArchivni");
				contact.IsBasicContact = reader.GetValue<bool>("ZakladniSubjekt");
				contact.ExternalCode = reader.GetValue<string>("ExternalCode");
				contact.HasNoVatForInvoicesIssued = reader.GetValue<bool>("FakturyVystaveneBezDph");
			}

			unitOfWork.Commit();
		}
	}
}
