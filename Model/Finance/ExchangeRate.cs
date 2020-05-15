using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Havit.GoranG3.Model.Finance
{
	public class ExchangeRate
	{
		public int Id { get; set; }

		public Currency Currency { get; set; }
		public int CurrencyId { get; set; }

		public DateTime DateFrom { get; set; }

		/// <summary>
		/// [ForeignCurrency]*[Rate] = [HomeCurrency]
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal Rate { get; set; }
	}
}