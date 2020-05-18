using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	/// <summary>
	/// G2: BankovniUcet
	/// </summary>
	[Cache(Priority = CacheItemPriority.Low)]
	public class BankAccount
    {
		public int Id { get; set; }

		/// <summary>
		/// G2: Nazev
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// G2: Banka
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string BankName { get; set; }

		/// <summary>
		/// G2: CisloUctu
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string AccountNumber { get; set; }

		[MaxLength(50)]
		public string Iban { get; set; }

		[MaxLength(50)]
		public string SwiftBic { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
