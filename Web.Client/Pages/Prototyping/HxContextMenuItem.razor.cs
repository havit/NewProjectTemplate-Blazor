using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public class HxContextMenuItem<T> : ComponentBase
    {
		[Parameter]
		public string Title { get; set; }

		[Parameter]
		public T CommandArgument { get; set; }

		[Parameter]
		public EventCallback<EventArgs> OnClick { get; set; }
	}
}
