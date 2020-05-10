using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class InvoiceList : PageComponentBase
    {
		protected List<InvoiceDto> Invoices { get; set; }
		protected InvoiceDto CurrentInvoice { get; set; }
		protected InvoiceListFilterDto Filter { get; set; }

		protected readonly IEnumerable<KeyValuePair<string, string>> NamedViews = new Dictionary<string, string>()
		{
			{ "ThisYear", "Letos vystavené" },
			{ "PaymentDue", "Neuhrazené po splatnosti" },
			{ "PaymentDue30Days", "Po splatnosti > 30 dnů" },
		};

		public override string Title => "Faktury vystavené";

		protected Task NamedViewSelected(EventArgs eventArgs)
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task FilterChanged(EventArgs eventArgs)
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task SearchRequested(EventArgs eventArgs)
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task NewInvoiceClicked(EventArgs eventArgs)
		{
			this.CurrentInvoice = new InvoiceDto();
			// OpenDetail() ?
			return Task.CompletedTask;
		}

		protected Task DeleteClicked(EventArgs eventArgs)
		{
			return Task.CompletedTask;
		}

		protected Task DuplicateClicked(EventArgs eventArgs)
		{
			return Task.CompletedTask;
		}

		protected Task PrintClicked(EventArgs eventArgs)
		{
			return Task.CompletedTask;
		}
	}
}
