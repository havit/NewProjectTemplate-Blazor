using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.GrpcTests
{
	[ServiceContract]
	public interface ITestFacade
	{
		ValueTask<DoSomethingResult> DoSomething(DoSomethingRequest request);

		Task AddRole();

		Task<Dto<string>> TryGetResult();

		Task RaiseOperationFailedException();

		Task RaiseInvalidOperationException();
	}
}
