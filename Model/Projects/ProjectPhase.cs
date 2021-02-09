using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Projects
{
	/// <summary>
	/// G2: Faze
	/// </summary>
	public class ProjectPhase
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: FazeLocalization.Nazev
		/// </summary>
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// G2: Kod
		/// </summary>
		[Required]
		[MaxLength(20)]
		public string Code { get; set; }

		/// <summary>
		/// G2: Poradi
		/// </summary>
		public int UiOrder { get; set; }

		public int? MigrationId { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
