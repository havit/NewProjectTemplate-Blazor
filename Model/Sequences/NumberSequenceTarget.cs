using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Sequences
{
	/// <summary>
	/// G2: CiselnaRadaTarget
	/// </summary>
	[Flags]
	public enum NumberSequenceTarget
	{
		InvoiceIssued = 0b_0000_0001,
		InvoiceReceived = 0b_0000_0010
	}
}
