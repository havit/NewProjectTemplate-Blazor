using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public class CurrencyLocalizer : DelegatingStringLocalizer<CurrencyLocalizer>, ICurrencyLocalizer
	{
		public CurrencyLocalizer(IStringLocalizer<CurrencyLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		// string-API properties
		public LocalizedString Code => this["Code"];
		public LocalizedString DefaultBankAccount => this["DefaultBankAccount"];
		public LocalizedString Delete => this["Delete"];
		public LocalizedString DeleteConfirmation => this["DeleteConfirmation"];
		public LocalizedString New => this["New"];
		public LocalizedString Plural => this["Plural"];
		public LocalizedString Singular => this["Singular"];
	}
}
