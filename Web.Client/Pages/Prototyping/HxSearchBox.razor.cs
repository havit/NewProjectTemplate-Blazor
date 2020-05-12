using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public class HxSearchBox : ComponentBase
    {
		[Parameter]
		public string QueryText { get; set; }

		[Parameter]
		public EventCallback<EventArgs> OnQueryTextChanged { get; set; }

	}
}
