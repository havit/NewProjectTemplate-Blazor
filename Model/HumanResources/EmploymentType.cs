using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Havit.GoranG3.Model.HumanResources
{
	/// <summary>
	/// Employee / Contractor / ...
	/// G2: PracovniVztah
	/// </summary>
	public class EmploymentType
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// CZ: Procentuální odvody firmy za pracovníka. Za IČAře 0%, za zaměstnance dle aktuálních sazeb sociálního a zdravotního pojištění (34%). Používá se pro automatický výpočet hodinovky na osobní kartě.
		/// G2: OdvodyZamestnavatele
		/// </summary>
		[Column(TypeName = "decimal(9, 5)")]
		public decimal EmployerContributionsRate { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}