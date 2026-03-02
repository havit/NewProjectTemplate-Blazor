namespace Aspire.Tests.Infrastructure;

/// <summary>
/// Starts and stops the single AppHost instance shared by all tests in the assembly.
/// </summary>
[TestClass]
public class TestAssemblySetup
{
	private static readonly TimeSpan StartupTimeout = TimeSpan.FromMinutes(5);

	/// <summary>The single AppHost instance used by all test classes.</summary>
	public static DistributedApplication App { get; private set; }

	[AssemblyInitialize]
	public static async Task AssemblyInitialize(TestContext context)
	{
		var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AppHost>();

		appHost.Services.AddLogging(logging =>
		{
			logging.SetMinimumLevel(LogLevel.Debug);
			logging.AddFilter(appHost.Environment.ApplicationName, LogLevel.Debug);
			logging.AddFilter("Aspire.", LogLevel.Debug);
		});

		var ct = context.CancellationToken;
		App = await appHost.BuildAsync(ct).WaitAsync(StartupTimeout, ct);
		await App.StartAsync(ct).WaitAsync(StartupTimeout, ct);
	}

	[AssemblyCleanup]
	public static async Task AssemblyCleanup()
	{
		if (App is not null)
		{
			await App.DisposeAsync();
		}
	}
}
