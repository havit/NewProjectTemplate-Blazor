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
	public record BankAccountDto
	{
		[DataMember(Order = 1)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		[DataMember(Order = 2)]
		public string Name { get; set; }

		[Required]
		[MaxLength(50)]
		[DataMember(Order = 3)]
		public string BankName { get; set; }

		[Required]
		[MaxLength(50)]
		[DataMember(Order = 4)]
		public string AccountNumber { get; set; }

		[MaxLength(50)]
		[DataMember(Order = 5)]
		public string Iban { get; set; }

		[MaxLength(50)]
		[DataMember(Order = 6)]
		public string SwiftBic { get; set; }
	}
}
