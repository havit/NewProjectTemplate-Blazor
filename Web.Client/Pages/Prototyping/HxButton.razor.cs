using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
	public class HxButton : ComponentBase
	{
		[Parameter]
		public string Text { get; set; }

		[Parameter]
		public string Skin { get; set; }

		[Parameter]
		public EventCallback<EventArgs> OnClick { get; set; }
	}
}
