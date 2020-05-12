using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public class HxContextMenu : ComponentBase
    {
		[Parameter]
		public RenderFragment ChildContent { get; set; }
	}
}
