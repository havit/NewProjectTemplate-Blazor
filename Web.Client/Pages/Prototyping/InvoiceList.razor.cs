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
		protected List<InvoiceDto> Invoices { get; set; } = Enumerable.Range(0, 250)
			.Select(i => new InvoiceDto
			{
				InvoiceId = i,
				InvoiceNumber = $"{i}/2000",
				IssuedDate = new DateTime(2020, 1, 1).AddDays(3 * i),
				TaxDate = new DateTime(2020, 1, 1).AddDays(3 * i),
				BusinessPartnerName = $"Zákazník {i}",
				Description = "Provoz aplikací v Azure"
			})
			.ToList();

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

		protected Task FilterChanged()
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task SearchRequested()
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task NewInvoiceClicked()
		{
			this.CurrentInvoice = new InvoiceDto();
			// OpenDetail() ?
			return Task.CompletedTask;
		}

		protected Task DeleteClicked(InvoiceDto invoiceDto)
		{
			return Task.CompletedTask;
		}

		protected Task DuplicateClicked(InvoiceDto invoiceDto)
		{
			return Task.CompletedTask;
		}

		protected Task PrintClicked(InvoiceDto invoiceDto)
		{
			return Task.CompletedTask;
		}
	}
}
