﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts
{
	[DataContract]
	public class Dto<TValue>
	{
		[DataMember(Order = 1)]
		public TValue Value { get; set; }

		public Dto()
		{
			// NOOP				
		}

		public Dto(TValue value)
		{
			this.Value = value;
		}
	}

	public static class Dto
	{
		public static Dto<TValue> FromValue<TValue>(TValue value)
		{
			return new Dto<TValue>(value);
		}
	}
}
