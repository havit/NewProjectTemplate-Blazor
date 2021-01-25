using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public class ExchangeRateLocalizer : DelegatingStringLocalizer<ExchangeRateLocalizer>, IExchangeRateLocalizer
	{
		public ExchangeRateLocalizer(IStringLocalizer<ExchangeRateLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		public LocalizedString Currency => this["Currency"];
		public LocalizedString DateFrom => this["DateFrom"];
		public LocalizedString Plural => this["Plural"];
		public LocalizedString Singular => this["Singular"];
		public LocalizedString Rate => this["Rate"];
	}
}
