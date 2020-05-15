using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Crm
{
	/// <summary>
	/// Person or organization.
	/// G2: Subjekt
	/// </summary>
	[Cache]
	public class Contact
    {
		public int Id { get; set; }
	}
}
