using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public class InvoiceListFilterDto
    {
		public DateRange IssuedDateRange { get; set; }
		public DateRange TaxDateRange { get; set; }
		public int ProjectId { get; set; }
		public int BusinessPartnerId { get; set; }
		public int InvoiceTypeId { get; set; }
	}
}
