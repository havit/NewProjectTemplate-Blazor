using Havit.GoranG3.Contracts.GrpcTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Facades.GrpcTests
{
	public class TestFacade : ITestFacade
	{
		public ValueTask<DoSomethingResult> DoSomething(DoSomethingRequest request)
		{
			return new ValueTask<DoSomethingResult>(new DoSomethingResult()
			{
				Message = request.Message + " response",
				Value = request.Value + 1
			});
		}
	}
}
