using Havit.Blazor.Components.Web.Bootstrap.Filters;
using Havit.Blazor.Components.Web.Bootstrap.Grids;
using Havit.Blazor.Components.Web.Bootstrap.NamedViews;
using Havit.GoranG3.Contracts.Finance.Invoices;
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
		protected int CurrentPageIndex { get; set; }
		protected SortingItem<InvoiceListDto>[] CurrentSorting { get; set; }

		protected List<InvoiceListDto> Invoices { get; set; } 
		protected int TotalInvoices { get; set; }

		protected InvoiceListDto CurrentInvoice { get; set; }
		protected GetInvoicesFilterDto Filter { get; set; } = new GetInvoicesFilterDto();

		[Inject]
		protected IInvoiceFacade InvoiceFacade { get; set; }

		protected readonly IEnumerable<NamedView<GetInvoicesFilterDto>> NamedViews = new List<NamedView<GetInvoicesFilterDto>>()
		{
			new NamedView<GetInvoicesFilterDto>("Letos vystavené", () => new GetInvoicesFilterDto { IssuedDateFrom = new DateTime(2020, 1, 1), IssuedDateTo = new DateTime(2020, 12, 31 ) } ),
			new NamedView<GetInvoicesFilterDto>("Neuhrazené po splatnosti", () => new GetInvoicesFilterDto { Text = "Neuhrazené po splatnosti" }),
			new NamedView<GetInvoicesFilterDto>("Po splatnosti > 30 dnů", () => new GetInvoicesFilterDto { Text = "Po splatnosti > 30 dnů" })
		};

		private async Task LoadInvoices()
		{
			// TODO: bázová třída, konstruktor?
			var request = new GetInvoicesRequest()
			{
				PageIndex = CurrentPageIndex, // TODO nechceme sledovat dvě proměnné, raději jednu a tu předat konstruktoru (což nám ještě generikum zavaří, ale to půjde...)
				//SortItems = currentSorting.Select(item => new Contracts.Common.SortItemDto { SortString = item.SortString, SortDirection = item.SortDirection }).ToList(), // TODO
				Filter = this.Filter
			};
			var result = await InvoiceFacade.GetInvoices(request);
			Invoices = result.Invoices; // TODO: Potřebujeme hodnoty rozebírat? Nestačil by nám result?
			TotalInvoices = result.TotalCount;
		}

		protected async Task HandleDataReloadRequired(/*GridUserState<InvoiceListDto> gridUserState*/) // Zahazujeme stav gridu, protože máme i jiné scénáře, odkud je tento stav k ničemu
		{
			await LoadInvoices();
		}

		protected async Task ApplyFilterRequested()
		{
			await LoadInvoices();
		}

		protected async Task NamedViewSelected(/*NamedView<GetInvoicesFilterDto> namedView*/)
		{
			await LoadInvoices();
		}

		protected Task SearchRequested()
		{
			// Tady by bylo něco jako BindData()
			return Task.CompletedTask;
		}

		protected Task NewInvoiceClicked()
		{
			this.CurrentInvoice = new InvoiceListDto();
			// OpenDetail() ?
			return Task.CompletedTask;
		}

		protected Task EditClicked(InvoiceListDto invoiceDto)
		{
			return Task.CompletedTask;
		}

		protected Task DeleteClicked(InvoiceListDto invoiceDto)
		{
			return Task.CompletedTask;
		}

		protected Task DuplicateClicked(InvoiceListDto invoiceDto)
		{
			return Task.CompletedTask;
		}

	}
}
