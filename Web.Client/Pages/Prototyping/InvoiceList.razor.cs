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

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
	public partial class InvoiceList
	{
		protected GridUserState<InvoiceListDto> CurrentGridState { get; set; } = new GridUserState<InvoiceListDto>(0, null); // TODO: Default nastavit v HxGridu
		protected List<InvoiceListDto> Invoices { get; set; }
		protected int TotalInvoices { get; set; }

		protected InvoiceListDto CurrentInvoice { get; set; }
		protected GetInvoicesFilterDto Filter { get; set; } = new GetInvoicesFilterDto();

		[Inject] protected IInvoiceFacade InvoiceFacade { get; set; }

		[Inject] protected NavigationManager NavigationManager { get; set; }

		protected readonly IEnumerable<NamedView<GetInvoicesFilterDto>> NamedViews = new List<NamedView<GetInvoicesFilterDto>>()
		{
			new NamedView<GetInvoicesFilterDto>("Letos vystavené", () => new GetInvoicesFilterDto { IssuedDateFrom = new DateTime(2020, 1, 1), IssuedDateTo = new DateTime(2020, 12, 31 ) } ),
			new NamedView<GetInvoicesFilterDto>("Neuhrazené po splatnosti", () => new GetInvoicesFilterDto { Text = "Neuhrazené po splatnosti" }),
			new NamedView<GetInvoicesFilterDto>("Po splatnosti > 30 dnů", () => new GetInvoicesFilterDto { Text = "Po splatnosti > 30 dnů" })
		};

		private CancellationTokenSource cancellationTokenSource;
		private async Task LoadInvoices()
		{
			cancellationTokenSource?.Cancel();
			cancellationTokenSource = new CancellationTokenSource();

			GetInvoicesRequest request = new GetInvoicesRequest()
			{
				Filter = this.Filter,
				PageIndex = this.CurrentGridState.PageIndex,
				SortItems = this.CurrentGridState.Sorting?.Select(item => new Contracts.Common.SortItemDto { SortString = item.SortString, SortDirection = item.SortDirection }).ToList()
			};

			try
			{
				var result = await InvoiceFacade.GetInvoices(request, cancellationTokenSource.Token);
				Invoices = result.Invoices; // TODO: Potřebujeme hodnoty rozebírat? Nestačil by nám result?
				TotalInvoices = result.TotalCount;
			}
			catch (RpcException e) when (e.Message?.Contains("AccessTokenNotAvailableException") ?? false)
			{
				// TODO GrpcClientInterceptor?
				// ex.Redirect();
				NavigationManager.NavigateTo("/authentication/login");
			}
			catch (RpcException e) when (e.StatusCode == StatusCode.Cancelled)
			{
				// NOOP
			}
		}

		protected async Task HandleDataReloadRequired()
		{
			await LoadInvoices();
		}

		// TODO: Nekolik volání metody LoadInvoices. Jak to napojit? Ideálně bez nutnosti řádky kódu.
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
