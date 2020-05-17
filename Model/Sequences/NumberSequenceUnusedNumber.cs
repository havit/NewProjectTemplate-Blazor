using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Sequences
{
	/// <summary>
	/// G2: CiselnaRadaVolneCislo
	/// </summary>
	public class NumberSequenceUnusedNumber
    {
		public int Id { get; set; }

		public NumberSequence NumberSequence { get; set; }
		public int NumberSequenceId { get; set; }

		public int Value { get; set; }
	}
}
