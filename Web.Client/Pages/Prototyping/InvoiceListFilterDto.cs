using Havit.Blazor.Components.Web.Bootstrap.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
	public class InvoiceListFilterDto
	{
		public DateTime? IssuedDateFrom { get; set; }
		public DateTime? IssuedDateTo { get; set; }
		public DateTime? TaxDateFrom { get; set; }
		public DateTime? TaxDateTo { get; set; }
		public string Text { get; set; }
	}
}
