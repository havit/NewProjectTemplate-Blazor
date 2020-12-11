using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.Model.Finance;
using Havit.Services.TimeServices;

namespace Havit.GoranG3.DataLayer.Seeds.Demo.Finance
{
	public class BankAccountSeed : DataSeed<DemoProfile>
	{
		private readonly ITimeService timeService;

		public BankAccountSeed(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public override void SeedData()
		{
			var bankAccounts = new[]
			{
				new BankAccount()
				{
					AccountNumber = "1234567944/2700",
					BankName = "UniCredit Bank",
					Name = "UniCredit CZK",
					SwiftBic = "BACXCZPP",
					Iban = "CZ34 2700 0000 0012 3456 7944",
					MigrationId = (int)Entry.UnicreditCzk,
					Created = timeService.GetCurrentTime(),
				},
				new BankAccount()
				{
					AccountNumber = "1234567979/2700",
					BankName = "UniCredit Bank",
					Name = "UniCredit EUR",
					SwiftBic = "BACXCZPP",
					Iban = "CZ59 2700 0000 0012 3456 7979",
					MigrationId = (int)Entry.UnicreditEur,
					Created = timeService.GetCurrentTime(),
				}
			};

			Seed(For(bankAccounts).PairBy(ba => ba.MigrationId));
		}

		public enum Entry
		{
			UnicreditCzk = -1_000_001,
			UnicreditEur = -1_000_002
		}
	}
}
