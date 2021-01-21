using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Collections;
using Havit.GoranG3.Contracts.Finance.Invoices;
using Havit.Linq;

namespace Havit.GoranG3.Facades.Finance.Invoices
{
	public class InvoiceFacade : IInvoiceFacade
	{
		public async Task<GetInvoicesResult> GetInvoices(GetInvoicesRequest request, CancellationToken cancellationToken)
		{
			await Task.Delay(500, cancellationToken);

			// use IQueryable, not List
			List<InvoiceListDto> invoices = Enumerable.Range(0, 250)
				.Select(i => new InvoiceListDto
				{
					InvoiceId = i,
					InvoiceNumber = $"{i}/2000",
					IssuedDate = new DateTime(2020, 1, 1).AddDays(3 * i),
					TaxDate = new DateTime(2020, 1, 1).AddDays(3 * i),
					BusinessPartnerName = $"Zákazník {i}",
					Description = "Provoz aplikací v Azure " + (new DateTime(2020, 1, 1).AddDays(3 * i)).ToString("MM/yyyy")
				})
				.WhereIf(request.Filter.IssuedDateFrom != null, invoice => invoice.IssuedDate >= request.Filter.IssuedDateFrom)
				.WhereIf(request.Filter.IssuedDateTo != null, invoice => invoice.IssuedDate <= request.Filter.IssuedDateTo)
				.WhereIf(request.Filter.TaxDateFrom != null, invoice => invoice.TaxDate >= request.Filter.TaxDateFrom)
				.WhereIf(request.Filter.TaxDateTo != null, invoice => invoice.TaxDate <= request.Filter.TaxDateTo)
				.WhereIf(!String.IsNullOrEmpty(request.Filter.Text), invoice => invoice.Description.Contains(request.Filter.Text, StringComparison.CurrentCultureIgnoreCase))
				.ToList();

			// Just a demo, do not use this approach in production. NEVER!
			if (request.SortItems.Any())
			{
				invoices.Sort(new GenericPropertyComparer<InvoiceListDto>(request.SortItems.Select(item => new SortItem(item.SortString, item.SortDirection)).ToList()));
			}

			return new GetInvoicesResult
			{
				Invoices = invoices.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList(),
				TotalCount = invoices.Count
			};
		}
	}
}
