using Havit.NewProjectTemplate.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.NewProjectTemplate.TestsForLocalDebugging;

/// <summary>
/// Bázový třída pro testy.
/// Zpřístupňuje nakonfigurovaný DI container.
/// </summary>
public class TestForLocalDebuggingBase : IntegrationTestBase
{
	[TestInitialize]
	public override void TestInitialize()
	{
#if !DEBUG
#pragma warning disable CS0162 // Unreachable Code Detected
		throw new InvalidOperationException("Test from TestsForLocalDebugging must not run in RELEASE.");
#endif
		base.TestInitialize();

		// ...application specific registrations to Container
	}
}
