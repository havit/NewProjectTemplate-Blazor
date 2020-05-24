using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
    public class VatRate
    {
		public int Id { get; set; }

		/// <summary>
		/// Nornalized rate (0.22 = 22%)
		/// </summary>
		[Column(TypeName = "decimal(9, 5)")]
		public Decimal Rate { get; set; }

		[Column(TypeName = "date")]
		public DateTime? ValidFrom { get; set; }

		[Column(TypeName = "date")]
		public DateTime? ValidTo { get; set; }
	}
}
