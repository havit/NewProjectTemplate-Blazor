using Havit.NewProjectTemplate.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.NewProjectTemplate.TestsForLocalDebugging;

public class TestForLocalDebuggingBase : IntegrationTestBase
{
	[TestInitialize]
	public override async Task TestInitializeAsync()
	{
#if !DEBUG
#pragma warning disable CS0162 // Unreachable Code Detected
		throw new InvalidOperationException("Test from TestsForLocalDebugging must not run in RELEASE.");
#endif
		await base.TestInitializeAsync();

		// ...application specific registrations to Container
	}
}
