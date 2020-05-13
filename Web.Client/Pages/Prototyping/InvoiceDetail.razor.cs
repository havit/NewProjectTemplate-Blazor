using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class InvoiceDetail : ComponentBase
    {
		[Parameter]
		public InvoiceDto Invoice { get; set; }

		[Parameter]
		public EventCallback<InvoiceDto> InvoiceChanged { get; set; }
	}
}
