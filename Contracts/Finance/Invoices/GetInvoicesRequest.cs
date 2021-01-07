using System.Collections.Generic;
using System.Runtime.Serialization;
using Havit.Collections;
using Havit.GoranG3.Contracts.Common;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[DataContract]
	public class GetInvoicesRequest
	{
		[DataMember(Order = 0)]
		public int PageSize { get; set; } = 20;

		[DataMember(Order = 1)]
		public int PageIndex { get; set; }

		[DataMember(Order = 2)]
		public List<SortItemDto> SortItems { get; set; } = new List<SortItemDto>();

		[DataMember(Order = 3)]
		public GetInvoicesFilterDto Filter { get; set; }

	}
}