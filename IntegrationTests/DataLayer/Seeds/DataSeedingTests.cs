﻿using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Microsoft.Extensions.DependencyInjection;
using Havit.NewProjectTemplate.TestHelpers;

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
		await seedRunner.SeedDataAsync<CoreProfile>(cancellationToken: TestContext.CancellationTokenSource.Token);

		// assert
		// no exception
	}
}
