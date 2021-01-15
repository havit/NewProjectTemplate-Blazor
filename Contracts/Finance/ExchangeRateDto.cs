using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	public record ExchangeRateDto
	{
		public int Id { get; set; }

		[Required]
		public int? CurrencyId { get; set; }

		[Required]
		public DateTime? DateFrom { get; set; }

		/// <summary>
		/// [ForeignCurrency]*[Rate] = [HomeCurrency]
		/// </summary>
		[Required]
		public decimal? Rate { get; set; }

		public void UpdateFrom(ExchangeRateDto other)
		{
			Id = other.Id;
			CurrencyId = other.CurrencyId;
			DateFrom = other.DateFrom;
			Rate = other.Rate;
		}
	}
}
