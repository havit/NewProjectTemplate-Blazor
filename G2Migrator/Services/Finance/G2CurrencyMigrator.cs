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
	public class G2CurrencyMigrator : IG2CurrencyMigrator
	{
		private readonly MigrationOptions options;
		private readonly ICurrencyRepository currencyRepository;
		private readonly IBankAccountRepository bankAccountRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2CurrencyMigrator(
			IOptions<MigrationOptions> options,
			ICurrencyRepository currencyRepository,
			IBankAccountRepository bankAccountRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.currencyRepository = currencyRepository;
			this.bankAccountRepository = bankAccountRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateCurrencies()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT CurrencyID, VychoziBankovniUcetID, Symbol, Created, Deleted FROM Currency", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var currencies = currencyRepository.GetAllIncludingDeleted();
			var bankAccounts = bankAccountRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var currencyID = reader.GetValue<int>("CurrencyID");
				Console.Write("Currency: " + currencyID);
				var currency = currencies.Find(p => p.MigrationId == currencyID);
				if (currency == null)
				{
					currency = new Currency();
					currency.MigrationId = currencyID;
					unitOfWork.AddForInsert(currency);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(currency);
					Console.WriteLine(" UPDATE");
				}

				BankAccount defaultBankAccount = null;
				if (reader["VychoziBankovniUcetID"] != DBNull.Value)
				{
					defaultBankAccount = bankAccounts.Find(p => p.MigrationId == (int)reader["VychoziBankovniUcetID"]);
					currency.DefaultBankAccount = defaultBankAccount;
				}
				currency.Code = reader.GetValue<string>("Symbol");
				currency.Created = reader.GetValue<DateTime>("Created");
				currency.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
