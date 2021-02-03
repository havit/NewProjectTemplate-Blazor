using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.GrpcTests;

namespace Havit.GoranG3.Web.Client.Infrastructure.FacadeDecorators
{
	public class TestFacadeClientDecorator : ITestFacade
	{
		private readonly ITestFacade innerFacade;
		private readonly IHxMessengerService messenger;

		public TestFacadeClientDecorator(ITestFacade innerFacade, IHxMessengerService messenger)
		{
			this.innerFacade = innerFacade;
			this.messenger = messenger;
		}

		public async Task AddRole()
		{
			try
			{
				await innerFacade.AddRole();
			}
			catch (OperationFailedException ex)
			{
				messenger.AddError(ex.Message);
				throw;
			}
		}

		public async ValueTask<DoSomethingResult> DoSomething(DoSomethingRequest request)
		{
			try
			{
				return await innerFacade.DoSomething(request);
			}
			catch (OperationFailedException ex)
			{
				messenger.AddError(ex.Message);
				throw;
			}
		}

		public async Task RaiseOperationFailedException()
		{
			try
			{
				await innerFacade.RaiseOperationFailedException();
			}
			catch (OperationFailedException ex)
			{
				messenger.AddError(ex.Message + "FROM DECORATOR");
				throw;
			}
		}

		public async Task<Dto<string>> TryGetResult()
		{
			try
			{
				return await innerFacade.TryGetResult();
			}
			catch (OperationFailedException ex)
			{
				messenger.AddError(ex.Message);
				throw;
			}
		}
	}
}
