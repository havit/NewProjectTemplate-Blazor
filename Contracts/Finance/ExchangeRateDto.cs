using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance
{
	[DataContract]
	public record ExchangeRateDto
	{
		[DataMember(Order = 1)]
		public int Id { get; set; }

		[Required]
		[DataMember(Order = 2)]
		public int? CurrencyId { get; set; }

		[Required]
		[DataMember(Order = 3)]
		public DateTime? DateFrom { get; set; }

		/// <summary>
		/// [ForeignCurrency]*[Rate] = [HomeCurrency]
		/// </summary>
		[Required]
		[DataMember(Order = 4)]
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
