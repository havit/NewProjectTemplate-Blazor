using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.NewProjectTemplate.Model.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havit.NewProjectTemplate.Web.Server.Controllers
{
	[ApiController]
	public class DependencyInjectionTestController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public DependencyInjectionTestController(
			IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpGet("api/di-test")]
		public void Test()
		{
			var user = new User()
			{
				Username = "test"
			};

			unitOfWork.AddForInsert(user);
			unitOfWork.Commit();
		}
	}
}
