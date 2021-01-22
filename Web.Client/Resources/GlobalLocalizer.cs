using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public class GlobalLocalizer : DelegatingStringLocalizer<GlobalLocalizer>, IGlobalLocalizer
	{
		public GlobalLocalizer(IStringLocalizer<GlobalLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		public LocalizedString DeleteSuccess => this["DeleteSuccess"];
		public LocalizedString NewSuccess => this["NewSuccess"];
		public LocalizedString UpdateSuccess => this["UpdateSuccess"];
	}
}
