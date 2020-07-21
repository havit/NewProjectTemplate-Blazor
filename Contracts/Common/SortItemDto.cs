using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Havit.Collections;

namespace Havit.GoranG3.Contracts.Common
{
	[DataContract]
	public class SortItemDto
	{
		[DataMember(Order = 1)]
		public string SortString { get; set; }

		[DataMember(Order = 2)]
		public SortDirection SortDirection { get; set; }
	}
}
