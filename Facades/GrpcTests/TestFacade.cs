using Havit.GoranG3.Contracts.GrpcTests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Facades.GrpcTests
{
	public class TestFacade : ITestFacade
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public TestFacade(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		public ValueTask<DoSomethingResult> DoSomething(DoSomethingRequest request)
		{
			var identity = httpContextAccessor.HttpContext.User.Identity;
			return new ValueTask<DoSomethingResult>(new DoSomethingResult()
			{
				Message = request.Message + " response, IsAuthenticated: " + identity.IsAuthenticated.ToString() + ", Name: " + identity.Name,
				Value = request.Value + 1
			});
		}
	}
}
