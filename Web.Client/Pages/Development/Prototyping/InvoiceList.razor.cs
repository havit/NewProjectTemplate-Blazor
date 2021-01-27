using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Common;
using Havit.GoranG3.Contracts.Finance.Invoices;
using Havit.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Havit.GoranG3.Web.Client.Pages.Development.Prototyping
{
	public partial class InvoiceList
	{
		[Inject] protected IInvoiceFacade InvoiceFacade { get; set; }

		[Inject] protected NavigationManager NavigationManager { get; set; }

		private InvoiceListDto currentInvoice;
		private GetInvoicesFilterDto filter = new GetInvoicesFilterDto();
		private HxGrid<InvoiceListDto> grid;

		private readonly IEnumerable<NamedView<GetInvoicesFilterDto>> namedViews = new List<NamedView<GetInvoicesFilterDto>>()
		{
			new NamedView<GetInvoicesFilterDto>("Letos vystavené", () => new GetInvoicesFilterDto { IssuedDateFrom = new DateTime(2020, 1, 1), IssuedDateTo = new DateTime(2020, 12, 31 ) } ),
			new NamedView<GetInvoicesFilterDto>("Neuhrazené po splatnosti", () => new GetInvoicesFilterDto { Text = "Neuhrazené po splatnosti" }),
			new NamedView<GetInvoicesFilterDto>("Po splatnosti > 30 dnů", () => new GetInvoicesFilterDto { Text = "Po splatnosti > 30 dnů" })
		};

		private CancellationTokenSource cancellationTokenSource;
		private async Task<GridDataProviderResult<InvoiceListDto>> InvoicesDataProvider(GridDataProviderRequest<InvoiceListDto> request)
		{
			cancellationTokenSource?.Cancel();
			cancellationTokenSource = new CancellationTokenSource();

			var getInvoicesRequest = new GetInvoicesRequest()
			{
				Filter = this.filter,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				SortItems = request.Sorting?.Select(item => new Contracts.Common.SortItemDto { SortString = item.SortString, SortDirection = item.SortDirection }).ToList()
			};

			var result = await InvoiceFacade.GetInvoices(getInvoicesRequest, cancellationTokenSource.Token);
			return new()
			{
				Data = result.Invoices,
				TotalCount = result.TotalCount
			};
		}

		// TODO: Nekolik volání metody LoadInvoices. Jak to napojit? Ideálně bez nutnosti řádky kódu.
		protected async Task ApplyFilterRequested()
		{
			await grid.RefreshDataAsync();
		}

		protected async Task NamedViewSelected(/*NamedView<GetInvoicesFilterDto> namedView*/)
		{
			await grid.RefreshDataAsync();
		}

		protected async Task SearchRequested()
		{
			await grid.RefreshDataAsync();
		}

		protected Task NewInvoiceClicked()
		{
			this.currentInvoice = new InvoiceListDto();
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
