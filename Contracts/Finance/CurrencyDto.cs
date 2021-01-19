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
	public record CurrencyDto
	{
		[DataMember(Order = 1)]
		public int Id { get; set; }

		[DataMember(Order = 2)]
		public int? DefaultBankAccountId { get; set; }

		[Required]
		[MaxLength(50)]
		[DataMember(Order = 3)]
		public string Code { get; set; }

		public void UpdateFrom(CurrencyDto other)
		{
			Id = other.Id;
			Code = other.Code;
			DefaultBankAccountId = other.DefaultBankAccountId;
		}
	}
}
