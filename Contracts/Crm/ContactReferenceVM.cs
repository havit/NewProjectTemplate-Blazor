using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Crm
{
	[DataContract]
	public record ContactReferenceVM
	{
		[DataMember(Order = 1)]
		public int Id { get; init; }

		[DataMember(Order = 2)]
		public string Name { get; init; }

		[DataMember(Order = 3)]
		public bool IsDeleted { get; init; }
	}
}
