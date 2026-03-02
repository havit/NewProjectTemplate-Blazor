namespace Aspire.Tests.Infrastructure;

/// <summary>
/// Base class for Aspire distributed integration tests.
///
/// A single AppHost is started once for the entire test assembly by
/// <see cref="TestAssemblySetup"/> and shared by all test classes.
/// Tests run sequentially — no parallel execution, no per-class isolation needed.
/// </summary>
[TestClass]
public abstract class IntegrationTestBase
{
	/// <summary>MSTest injects this automatically for every test method.</summary>
	public TestContext TestContext { get; set; }

	/// <summary>Alias matching the naming convention used in test methods.</summary>
	protected TestContext context => TestContext;

	/// <summary>The single AppHost instance shared across all test classes.</summary>
	protected DistributedApplication _app => TestAssemblySetup.App;

	/// <summary>
	/// Timeout for per-test operations such as WaitForResourceHealthyAsync.
	/// Override in a derived class if needed.
	/// </summary>
	protected virtual TimeSpan DefaultTimeout => TimeSpan.FromMinutes(3);
}
