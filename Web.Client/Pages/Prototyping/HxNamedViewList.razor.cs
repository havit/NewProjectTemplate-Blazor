using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class HxNamedViewList : ComponentBase
    {
		[Parameter]
		public IEnumerable<KeyValuePair<string, string>> Data { get; set; }

		[Parameter]
		public EventCallback<EventArgs> OnSelected { get; set; }
	}
}
