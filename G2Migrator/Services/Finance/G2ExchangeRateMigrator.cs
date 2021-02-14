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
	public class G2ExchangeRateMigrator : IG2ExchangeRateMigrator
	{
		private readonly MigrationOptions options;
		private readonly IExchangeRateRepository exchangeRateRepository;
		private readonly ICurrencyRepository currencyRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2ExchangeRateMigrator(
			IOptions<MigrationOptions> options,
			IExchangeRateRepository exchangeRateRepository,
			ICurrencyRepository currencyRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.exchangeRateRepository = exchangeRateRepository;
			this.currencyRepository = currencyRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateExchangeRates()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT ExchangeRateID, CurrencyID, DateFrom, Rate FROM ExchangeRate", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var exchangeRates = exchangeRateRepository.GetAll();
			var currencies = currencyRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var exchangeRateID = reader.GetValue<int>("ExchangeRateID");
				Console.Write("ExchangeRate: " + exchangeRateID);
				var exchangeRate = exchangeRates.Find(p => p.MigrationId == exchangeRateID);
				if (exchangeRate == null)
				{
					exchangeRate = new ExchangeRate();
					exchangeRate.MigrationId = exchangeRateID;
					unitOfWork.AddForInsert(exchangeRate);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(exchangeRate);
					Console.WriteLine(" UPDATE");
				}

				Currency currency = currencies.Find(p => p.MigrationId == (int)reader["CurrencyID"]);
				exchangeRate.Currency = currency;
				exchangeRate.DateFrom = reader.GetValue<DateTime>("DateFrom");
				exchangeRate.Rate = reader.GetValue<decimal>("Rate");
			}

			unitOfWork.Commit();
		}
	}
}
