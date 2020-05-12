using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
	public class HxCustomGridColumn : HxGridColumnBase
	{
		[Parameter]
		public RenderFragment Template { get; set; }
	}
}