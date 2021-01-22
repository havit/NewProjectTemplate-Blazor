using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public class BankAccountLocalizer : DelegatingStringLocalizer<BankAccountLocalizer>, IBankAccountLocalizer
	{
		public BankAccountLocalizer(IStringLocalizer<BankAccountLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		public LocalizedString AccountNumber => this["AccountNumber"];
		public LocalizedString BankName => this["BankName"];
		public LocalizedString Iban => this["Iban"];
		public LocalizedString Name => this["Name"];
		public LocalizedString SwiftBic => this["SwiftBic"];
		public LocalizedString New => this["New"];
		public LocalizedString Plural => this["Plural"];
		public LocalizedString Singular => this["Singular"];
		public LocalizedString Delete => this["Delete"];
		public LocalizedString DeleteConfirmation => this["DeleteConfirmation"];
	}
}
