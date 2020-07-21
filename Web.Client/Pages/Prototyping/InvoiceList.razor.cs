using Havit.Blazor.Components.Web.Bootstrap.Filters;
using Havit.Blazor.Components.Web.Bootstrap.NamedViews;
using Havit.Linq;
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

		protected readonly IEnumerable<NamedView<InvoiceListFilterDto>> NamedViews = new List<NamedView<InvoiceListFilterDto>>()
		{
			new NamedView<InvoiceListFilterDto>("Letos vystavené", () => new InvoiceListFilterDto { IssuedDateFrom = new DateTime(2020, 1, 1), IssuedDateTo = new DateTime(2020, 12, 31 ) } ),
			new NamedView<InvoiceListFilterDto>("Neuhrazené po splatnosti", () => new InvoiceListFilterDto { Text = "Neuhrazené po splatnosti" }),
			new NamedView<InvoiceListFilterDto>("Po splatnosti > 30 dnů", () => new InvoiceListFilterDto { Text = "Po splatnosti > 30 dnů" })
		};

		//public override string Title => "Faktury vystavené";



		protected override void OnInitialized()
		{
			base.OnInitialized();
			LoadInvoices();
		}

		private void LoadInvoices()
		{
			Invoices = Enumerable.Range(0, 250)
				.Select(i => new InvoiceDto
				{
					InvoiceId = i,
					InvoiceNumber = $"{i}/2000",
					IssuedDate = new DateTime(2020, 1, 1).AddDays(3 * i),
					TaxDate = new DateTime(2020, 1, 1).AddDays(3 * i),
					BusinessPartnerName = $"Zákazník {i}",
					Description = "Provoz aplikací v Azure " + (new DateTime(2020, 1, 1).AddDays(3 * i)).ToString("MM/yyyy")
				})
				.WhereIf(Filter.IssuedDateFrom != null, invoice => invoice.IssuedDate >= Filter.IssuedDateFrom)
				.WhereIf(Filter.IssuedDateTo != null, invoice => invoice.IssuedDate <= Filter.IssuedDateTo)
				.WhereIf(Filter.TaxDateFrom != null, invoice => invoice.TaxDate >= Filter.TaxDateFrom)
				.WhereIf(Filter.TaxDateTo != null, invoice => invoice.TaxDate <= Filter.TaxDateTo)
				.WhereIf(!String.IsNullOrEmpty(Filter.Text), invoice => invoice.Description.Contains(Filter.Text, StringComparison.CurrentCultureIgnoreCase))
				.ToList();
			StateHasChanged();
		}

		protected Task ApplyFilterRequested()
		{
			LoadInvoices();
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task NamedViewSelected(/*NamedView<InvoiceListFilterDto> namedView*/)
		{
			LoadInvoices();
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
			Invoices.Remove(invoiceDto);
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
