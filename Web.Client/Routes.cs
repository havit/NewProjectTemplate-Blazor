using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client
{
	public static class Routes
	{
		public static string Home = "/";

		public static class Administration
		{
			public const string Index = "/admin/";
		}

		public static class UserAdministration
		{
			public const string Currencies = "/admin/user/currencies";
			public const string ExchangeRates = "/admin/user/exchange-rates";
			public const string BankAccounts = "/admin/user/bank-accounts";
		}
	}
}
