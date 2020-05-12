using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class HxListLayout : ComponentBase
    {
		[Parameter]
		public RenderFragment SearchSection { get; set; }

		[Parameter]
		public RenderFragment FilterSection { get; set; }

		[Parameter]
		public RenderFragment NamedViewsSection { get; set; }

		[Parameter]
		public RenderFragment DataSection { get; set; }

		[Parameter]
		public RenderFragment DetailSection { get; set; }

		[Parameter]
		public RenderFragment CommandsSection { get; set; }
	}
}
