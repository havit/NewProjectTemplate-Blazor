using System.Collections.Generic;
using System.Runtime.Serialization;
using Havit.Collections;
using Havit.GoranG3.Contracts.Common;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[DataContract]
	public class GetInvoicesRequest
	{
		public const int PageSize = 20; // TODO: global pagesize? Je to dobře na requestu?
		
		[DataMember(Order = 1)]
		public int PageIndex { get; set; }
		
		[DataMember(Order = 2)]
		public List<SortItemDto> SortItems { get; set; } = new List<SortItemDto>();
		
		[DataMember(Order = 3)]
		public GetInvoicesFilterDto Filter { get; set; }

	}
}