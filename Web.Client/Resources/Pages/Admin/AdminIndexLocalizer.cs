using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Pages.Admin
{
	public class AdminIndexLocalizer : DelegatingStringLocalizer<AdminIndexLocalizer>, IAdminIndexLocalizer
	{
		public AdminIndexLocalizer(IStringLocalizer<AdminIndexLocalizer> innerLocalizer) : base(innerLocalizer)
		{
		}

		public LocalizedString CultureRemoved => this["CultureRemoved"];
	}
}
