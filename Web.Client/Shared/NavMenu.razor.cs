using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Web.Client.Resources;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Shared
{
	public partial class NavMenu
	{
		[Inject] protected INavigationLocalizer NavigationLocalizer { get; set; }

		private bool collapsed = true;

		private string GetCssClass() => collapsed ? "collapse" : null;

		private void ToggleNavMenu()
		{
			collapsed = !collapsed;
		}
	}
}
