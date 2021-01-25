using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.DataLayer.Repositories.Finance;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.DataLayer.Seeds.Demo.Finance
{
	public class ExchangeRateSeed : DataSeed<DemoProfile>
	{
		private readonly ICurrencyRepository currencyRepository;

		public ExchangeRateSeed(ICurrencyRepository currencyRepository)
		{
			this.currencyRepository = currencyRepository;
		}

		public override void SeedData()
		{
			var currencies = currencyRepository.GetAll().ToDictionary(c => c.Code);

			var exchangeRates = new[]
			{
				new ExchangeRate()
				{
					CurrencyId = currencies["USD"].Id,
					DateFrom = new DateTime(2021, 1, 1),
					Rate = 21.387m
				},
				new ExchangeRate()
				{
					CurrencyId = currencies["EUR"].Id,
					DateFrom = new DateTime(2021, 1, 1),
					Rate = 26.245m
				}
			};

			Seed(For(exchangeRates).PairBy(c => c.CurrencyId).AndBy(c => c.DateFrom));
		}

		public override IEnumerable<Type> GetPrerequisiteDataSeeds()
		{
			yield return typeof(CurrencySeed);
		}
	}
}
