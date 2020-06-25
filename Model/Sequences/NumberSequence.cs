using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Sequences
{
	/// <summary>
	/// G2: CiselnaRada
	/// </summary>
	public class NumberSequence
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(10)]
		public string Prefix { get; set; }

		[MaxLength(10)]
		public string Suffix { get; set; }

		/// <summary>
		/// Number of digits for padding. No padding if null.
		/// G2: PocetCislic
		/// </summary>
		public int? DigitCount { get; set; }

		/// <summary>
		/// G2: PocatecniHodnota
		/// </summary>
		public int InitialValue { get; set; } = 1;

		/// <summary>
		/// Last value used. Not used at all if null.
		/// G2: PosledniPouzitaHodnota
		/// </summary>
		public int? LastValue { get; set; }

		public bool IsActive { get; set; } = true;

		/// <summary>
		/// G2: PouzitelnaOd
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// G2: PouzitelnaDo
		/// </summary>
		public DateTime? EndDate { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }

		/// <summary>
		/// Targets map (flags).
		/// G2: Targets 
		/// </summary>
		public NumberSequenceTarget Targets { get; set; }

		/// <summary>
		/// Returns value formatted according to NumberSequence settings.
		/// </summary>
		private string FormatNumber(int value) // G2
		{
			string result = value.ToString("f0");

			if (this.DigitCount != null)
			{
				result = result.PadLeft(this.DigitCount.Value, '0');
			}

			if (!String.IsNullOrEmpty(this.Prefix))
			{
				result = this.Prefix + result;
			}

			if (!String.IsNullOrEmpty(this.Suffix))
			{
				result = result + this.Suffix;
			}

			return result;
		}
	}
}
