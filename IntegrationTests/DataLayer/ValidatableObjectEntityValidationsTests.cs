using Havit.Data.EntityFrameworkCore.Patterns.UnitOfWorks.EntityValidation;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.Model.Finance;
using Havit.GoranG3.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.IntegrationTests.DataLayer
{
	[TestClass]
	public class ValidatableObjectEntityValidationsTests : IntegrationTestBase
	{
		[TestMethod]
		[ExpectedException(typeof(ValidationFailedException))]
		public void ValidatableObjectEntityValidations_FailsForInvalidEntity()
		{
			// arrange
			var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();

			var invalidEntity = new Transaction()
			{
				TransactionStatus = TransactionStatus.Regular
			};

			// act
			unitOfWork.AddForInsert(invalidEntity);
			unitOfWork.Commit();

			// assert - exception expected
		}
	}
}
