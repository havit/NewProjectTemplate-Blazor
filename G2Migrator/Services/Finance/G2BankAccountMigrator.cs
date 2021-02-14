using System;
using System.Collections.Generic;
using System.Text;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Finance;
using Havit.NewProjectTemplate.Model.Finance;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Finance
{
	[Service]
	public class G2BankAccountMigrator : IG2BankAccountMigrator
	{
		private readonly MigrationOptions options;
		private readonly IBankAccountRepository bankAccountRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2BankAccountMigrator(
			IOptions<MigrationOptions> options,
			IBankAccountRepository bankAccountRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.bankAccountRepository = bankAccountRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateBankAccounts()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT BankovniUcetID, Nazev, Banka, CisloUctu, Iban, SwiftBic, Created, Deleted FROM BankovniUcet", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var bankAccounts = bankAccountRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var bankAccountID = reader.GetValue<int>("BankovniUcetID");
				Console.Write("BankovniUcet => BankAccount " + bankAccountID);
				var bankAccount = bankAccounts.Find(p => p.MigrationId == bankAccountID);
				if (bankAccount == null)
				{
					bankAccount = new BankAccount();
					bankAccount.MigrationId = bankAccountID;
					unitOfWork.AddForInsert(bankAccount);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(bankAccount);
					Console.WriteLine(" UPDATE");
				}

				bankAccount.Name = reader.GetValue<string>("Nazev");
				bankAccount.BankName = reader.GetValue<string>("Banka");
				bankAccount.AccountNumber = reader.GetValue<string>("CisloUctu");
				bankAccount.Iban = reader.GetValue<string>("Iban");
				bankAccount.SwiftBic = reader.GetValue<string>("SwiftBic");
				bankAccount.Created = reader.GetValue<DateTime>("Created");
				bankAccount.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
