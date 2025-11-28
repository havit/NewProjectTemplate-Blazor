using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Havit.NewProjectTemplate.TestHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.IntegrationTests.DataLayer.Seeds;

[TestClass]
public class DataSeedingTests : IntegrationTestBase
{
	protected override bool UseLocalDb => true;
	protected override bool DeleteDbData => true; // default, but to be sure :D

	[TestMethod]
	public async Task DataSeeds_CoreProfile()
	{
		// arrange
		var seedRunner = ServiceProvider.GetRequiredService<IDataSeedRunner>();

		// act
		await seedRunner.SeedDataAsync<CoreProfile>(cancellationToken: TestContext.CancellationToken);

		// assert
		// no exception
	}
}
