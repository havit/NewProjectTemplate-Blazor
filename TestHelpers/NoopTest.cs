namespace Havit.NewProjectTemplate.TestHelpers;

[TestClass]
public class NoopTest
{
	[TestMethod]
	public void TestHelpers_NoopTest()
	{
		// This is a placeholder test to ensure the test project builds successfully.
		// AI tooling gets nervous with completely empty test projects.
#pragma warning disable MSTEST0032 // Assertion condition is always true
		Assert.IsTrue(true);
#pragma warning restore MSTEST0032 // Assertion condition is always true
	}
}
