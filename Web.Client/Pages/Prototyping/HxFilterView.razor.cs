using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public class HxFilterView<T> : ComponentBase
    {
		[Parameter]
		public T Data { get; set; }

		[Parameter]
		public EventCallback<EventArgs> OnChange { get; set; }

		[Parameter]
		public RenderFragment<T> Criteria { get; set; }
	}
}
