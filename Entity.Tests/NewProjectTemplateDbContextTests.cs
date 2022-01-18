using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.NewProjectTemplate.Entity.Tests;

[TestClass]
public class NewProjectTemplateDbContextTests
{
	[TestMethod]
	public void NewProjectTemplateDbContext_CheckModelConventions()
	{
		// Arrange
		DbContextOptions<NewProjectTemplateDbContext> options = new DbContextOptionsBuilder<NewProjectTemplateDbContext>()
			.UseInMemoryDatabase(nameof(NewProjectTemplateDbContext))
			.Options;
		NewProjectTemplateDbContext dbContext = new NewProjectTemplateDbContext(options);

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
