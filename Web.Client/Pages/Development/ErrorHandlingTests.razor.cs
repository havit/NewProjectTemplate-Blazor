using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.GrpcTests;
using Microsoft.AspNetCore.Components;

namespace Havit.GoranG3.Web.Client.Pages.Development
{
	public partial class ErrorHandlingTests
	{
		[Inject] protected ITestFacade TestFacade { get; set; }

		[Inject] protected IHxMessengerService Messenger { get; set; }

		private async Task HandleWithTryCatch()
		{
			try
			{
				await TestFacade.RaiseOperationFailedException();
			}
			catch (OperationFailedException ex)
			{
				Messenger.AddInformation("Client code catch", ex.Message);
			}
		}
	}
}
