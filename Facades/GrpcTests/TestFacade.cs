using Havit.GoranG3.Contracts;
using Havit.GoranG3.Contracts.GrpcTests;
using Havit.GoranG3.Model.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<User> userManager;

		public TestFacade(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}

		public async Task AddRole()
		{
			var user = await userManager.FindByEmailAsync("haken@havit.cz");
			//await userManager.AddToRoleAsync(user, nameof(Role.Entry.SystemAdministrator));
			await userManager.AddToRoleAsync(user, nameof(Role.Entry.UserSettingsAdministrator));
		}

		public ValueTask<DoSomethingResult> DoSomething(DoSomethingRequest request)
		{
			var user = httpContextAccessor.HttpContext.User;

			var sb = new StringBuilder();
			sb.AppendLine(request.Message);
			sb.AppendLine($"IsAuthenticated: {user.Identity.IsAuthenticated}");
			sb.AppendLine($"Name: {user.Identity.Name}");
			sb.AppendLine($"IsInRole(SystemAdministrator): {user.IsInRole(nameof(Role.Entry.SystemAdministrator))}");
			sb.AppendLine($"IsInRole(UserSettingsAdministrator): {user.IsInRole(nameof(Role.Entry.UserSettingsAdministrator))}");

			return new ValueTask<DoSomethingResult>(new DoSomethingResult()
			{
				Message = sb.ToString(),
				Value = request.Value + 1
			});
		}

		public Task RaiseOperationFailedException()
		{
			throw new OperationFailedException("Blah blah OperationFailedException from server");
		}

		public Task<Dto<string>> TryGetResult()
		{
			return Task.FromResult(new Dto<string>() { Value = "tak co? tak co? tak co? tak co? tak co?" });
		}
	}
}
