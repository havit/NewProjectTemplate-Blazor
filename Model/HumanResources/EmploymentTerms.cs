using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Havit.GoranG3.Model.HumanResources
{
	public class EmploymentTerms
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: TypOdmenovani
		/// </summary>
		public EmployeeRateType RateType { get; set; }

		/// <summary>
		/// G2: DenniFondPracovniDoby
		/// </summary>
		[Column(TypeName = "decimal(9, 2)")]
		public decimal HourPerDay { get; set; } = 8;

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}