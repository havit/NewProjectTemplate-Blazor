using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[DataContract]
	public class InvoiceListDto
	{
		[DataMember(Order = 1)]
		public int InvoiceId { get; set; }
		
		[DataMember(Order = 2)]
		public DateTime IssuedDate { get; set; }
		
		[DataMember(Order = 3)]
		public DateTime TaxDate { get; set; }
		
		[DataMember(Order = 4)]
		public string InvoiceNumber { get; set; }
		
		[DataMember(Order = 5)]
		public string BusinessPartnerInvoiceNumber { get; set; }
		
		[DataMember(Order = 6)]
		public string Description { get; set; }
		
		[DataMember(Order = 7)]
		public string BusinessPartnerName { get; set; }
		
		[DataMember(Order = 8)]
		public decimal TotalExcludingVAT { get; set; } // TODO: Currency? Money?
		
		[DataMember(Order = 9)]
		public decimal TotalIncludingVAT { get; set; }
		
		[DataMember(Order = 10)]
		public decimal TotalPaid { get; set; }
		
		[DataMember(Order = 11)]
		public string Currency { get; set; } // TODO: Nebo číselník a odkaz? Výkon? Takto pohodlné a v pohodě pro zobrazení
		
		[DataMember(Order = 12)]
		public DateTime? PaidDate { get; set; }
		
		[DataMember(Order = 13)]
		public DateTime? DueDate { get; set; }
	}
}