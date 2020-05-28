using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	[Cache(Priority = CacheItemPriority.High)]
	public class Currency
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: BankovniUcet.VychoziBankovniUcet
		/// </summary>
		public BankAccount DefaultBankAccount { get; set; }
		public int? DefaultBankAccountId { get; set; }

		/// <summary>
		/// G2: Symbol
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string Code { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public List<ExchangeRate> ExchangeRates { get; } = new List<ExchangeRate>();

		public int? MigrationId { get; set; }
	}
}
