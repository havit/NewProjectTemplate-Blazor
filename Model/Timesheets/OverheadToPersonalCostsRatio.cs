using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Timesheets
{
	/// <summary>
	/// G2: RezijniPrirazkaOsobnichNakladu
	/// </summary>
	[Cache]
	public class OverheadToPersonalCostsRatio
    {
		public int Id { get; set; }

		public DateTime StartDate { get; set; }

		/// <summary>
		/// Normalized ratio [50% saved as 0.5]
		/// G2: KoeficientPrirazky
		/// </summary>
		[Column(TypeName = "decimal(9, 4)")]
		public decimal Ratio { get; set; }
	}
}
