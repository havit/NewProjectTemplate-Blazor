using System.Linq;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.DataLayer.Seeds.Core;
using Havit.GoranG3.Model.Common;
using Havit.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Havit.Services.Caching;

namespace Havit.GoranG3.TestsForLocalDebugging.DataLayer.Seeds
{
	[TestClass]
	public class DataSeedingTests : TestForLocalDebuggingBase
	{
		protected override bool UseLocalDb => true;

		//[TestMethod]
		[TestCategory("Explicit")]
		public void DataSeedRunner_SeedCoreProfile()
		{
			// arrange
			var dbContext = ServiceProvider.GetRequiredService<IDbContext>();
			var seedRunner = ServiceProvider.GetRequiredService<IDataSeedRunner>();
			dbContext.Database.EnsureDeleted();
			dbContext.Database.EnsureCreated();
			dbContext.Database.Migrate();

			// act
			seedRunner.SeedData<CoreProfile>();

			// assert
			// no exception
		}
	}
}
