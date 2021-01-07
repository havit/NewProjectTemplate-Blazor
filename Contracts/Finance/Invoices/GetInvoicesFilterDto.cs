using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[DataContract]
	public class GetInvoicesFilterDto
	{
		[DataMember(Order = 1)]
		public DateTime? IssuedDateFrom { get; set; }

		[DataMember(Order = 2)]
		public DateTime? IssuedDateTo { get; set; }

		[DataMember(Order = 3)]
		public DateTime? TaxDateFrom { get; set; }

		[DataMember(Order = 4)]
		public DateTime? TaxDateTo { get; set; }

		[DataMember(Order = 5)]
		public string Text { get; set; }
	}
}
