using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[DataContract]
	public class GetInvoicesResult
	{
		[DataMember(Order = 1)]
		public List<InvoiceListDto> Invoices { get; set; } = new List<InvoiceListDto>();

		[DataMember(Order = 2)]
		public int TotalCount { get; set; }
	}
}