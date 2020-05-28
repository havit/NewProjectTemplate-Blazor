using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Havit.GoranG3.Model.HumanResources
{
	/// <summary>
	/// G2: PracovnikHistorie
	/// </summary>
	public class EmployeeHistory
	{
		public int Id { get; set; }

		public Employee Employee { get; set; }
		public int EmployeeId { get; set; }

		/// <summary>
		/// G2: DatumOd
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// G2: PracovniPozice
		/// </summary>
		[MaxLength(50)]
		public string JobPosition { get; set; }

		/// <summary>
		/// Employee / Contractor / ...
		/// G2: PracovniVztah
		/// </summary>
		public EmploymentType EmploymentType { get; set; }
		public int EmploymentTypeId { get; set; }

		/// <summary>
		/// G2: SjednanaZakladniSazba
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal BasicRate { get; set; }

		/// <summary>
		/// Typ úvazku (určuje, k čemu se vztahuje SjednanaZakladniSazba - obvykle měsíční/hodinový).
		/// G2: TypUvazku
		/// </summary>
		public EmploymentTerms EmploymentTerms { get; set; }
		public int EmploymentTermsId { get; set; }

		/// <summary>
		/// G2: HodinovaSazbaOsobnichNakladu
		/// </summary>
		[Column(TypeName = "Money")]
		public Decimal HourlyCost { get; set; }

		/// <summary>
		/// Overrides global ratio (if set).
		/// G2: KoeficientPrirazkyRezie
		/// </summary>
		[Column(TypeName = "decimal(9, 4)")]
		public decimal? OverheadToPersonalCostsRatio { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }
	}
}