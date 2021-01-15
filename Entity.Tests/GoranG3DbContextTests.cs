using System;
using Havit.GoranG3.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.GoranG3.Entity.Tests
{
	[TestClass]
	public class GoranG3DbContextTests
	{
		[TestMethod]
		public void GoranG3DbContext_CheckModelConventions()
		{
			// Arrange
			DbContextOptions<GoranG3DbContext> options = new DbContextOptionsBuilder<GoranG3DbContext>()
				.UseInMemoryDatabase(nameof(GoranG3DbContext))
				.Options;
			GoranG3DbContext dbContext = new GoranG3DbContext(options);

			// Act
			Havit.Data.EntityFrameworkCore.ModelValidation.ModelValidator modelValidator = new Havit.Data.EntityFrameworkCore.ModelValidation.ModelValidator();
			string errors = modelValidator.Validate(dbContext);

			// Assert
			if (!String.IsNullOrEmpty(errors))
			{
				Assert.Fail(errors);
			}
		}
	}
}
