using Havit.Blazor.Components.Web.Bootstrap.NamedViews;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class InvoiceList
    {
		protected List<InvoiceDto> Invoices { get; set; }
		protected InvoiceDto CurrentInvoice { get; set; }
		protected InvoiceListFilterDto Filter { get; set; } = new InvoiceListFilterDto();


		protected readonly IEnumerable<NamedView> NamedViews = new List<NamedView>()
		{
			new NamedView("Letos vystavené"),
			new NamedView("Neuhrazené po splatnosti"),
			new NamedView("Po splatnosti > 30 dnů")
		};

		//public override string Title => "Faktury vystavené";

		protected Task NamedViewSelected(NamedView namedView)
		{
			//Filter.Reset();
			if (namedView == new NamedView() /* nějaká instance */)
			{
				//Filter.IssuedDateRange = null; /* atd. */
			}
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task FilterChanged(EventArgs eventArgs)
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task SearchRequested(string eventArgs)
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
