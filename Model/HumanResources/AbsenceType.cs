using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Havit.GoranG3.Model.Localizations;

namespace Havit.GoranG3.Model.HumanResources
{
	/// <summary>
	/// G2: TypPracovnihoVolna
	/// </summary>
	public class AbsenceType
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		public bool HasBalance { get; set; } = false;

		public bool IsActive { get; set; } = true;

		public int UiOrder { get; set; }

		public int? MigrationId { get; set; }
	}
}