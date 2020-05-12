using System;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
	public class InvoiceDto
	{
		public int InvoiceId { get; set; }
		public DateTime IssuedDate { get; set; }
		public DateTime TaxDate { get; set; }
		public string Description { get; set; }
		public string InvoiceNumber { get; set; }
		public string BusinessPartnerName { get; set; }
	}
}