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

		public LocalizedString DeleteRecord => this["DeleteRecord"];
		public LocalizedString DeleteConfirmation => this["DeleteConfirmation"];
		public LocalizedString DeleteSuccess => this["DeleteSuccess"];
		public LocalizedString NewSuccess => this["NewSuccess"];
		public LocalizedString NewRecord => this["NewRecord"];
		public LocalizedString UpdateSuccess => this["UpdateSuccess"];
		public LocalizedString SelectNull => this["SelectNull"];
		public LocalizedString SelectNullItems => this["SelectNullItems"];
	}
}
