using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Services.Infrastructure
{
	public class OperationFailedException : ApplicationException
	{
		public OperationFailedException(string message) : base(message)
		{
			// NOOP
		}

		public OperationFailedException(string message, Exception innerException) : base(message, innerException)
		{
			// NOOP
		}
	}
}
