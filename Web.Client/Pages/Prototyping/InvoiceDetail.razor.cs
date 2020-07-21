using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance.Invoices;
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
		public InvoiceListDto Invoice { get; set; }

		[Parameter]
		public EventCallback<InvoiceListDto> InvoiceChanged { get; set; }

		private void HandleSuggestionsRequested(SuggestionRequest request)
		{
			request.Suggestions = Enumerable.Range(0, 10).Select(i => request.UserInput + " " + (i + 1).ToString()).ToList();
		}
	}
}
