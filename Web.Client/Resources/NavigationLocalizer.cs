using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public class NavigationLocalizer : DelegatingStringLocalizer<NavigationLocalizer>, INavigationLocalizer
	{
		public NavigationLocalizer(IStringLocalizer<NavigationLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		public LocalizedString Administration => this["Administration"];
		public LocalizedString BankAccounts => this["BankAccounts"];
		public LocalizedString Currencies => this["Currencies"];
		public LocalizedString ExchangeRates => this["ExchangeRates"];
	}
}
