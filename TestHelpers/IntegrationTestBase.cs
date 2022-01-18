using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Havit.NewProjectTemplate.DependencyInjection;

namespace Havit.NewProjectTemplate.TestHelpers;

public class IntegrationTestBase
{
	private IDisposable scope;

	protected IServiceProvider ServiceProvider { get; private set; }

	protected virtual bool UseLocalDb => false;
	protected virtual bool DeleteDbData => true;

	protected virtual bool SeedData => true;

	[TestInitialize]
	public virtual void TestInitialize()
	{
		IServiceCollection services = CreateServiceCollection();
		IServiceProvider serviceProvider = services.BuildServiceProvider();

		scope = serviceProvider.CreateScope();

		var dbContext = serviceProvider.GetRequiredService<IDbContext>();
		if (DeleteDbData)
		{
			dbContext.Database.EnsureDeleted();
		}
		if (this.UseLocalDb)
		{
			dbContext.Database.Migrate();
		}

		if (this.SeedData)
		{
			var dataSeedRunner = serviceProvider.GetRequiredService<IDataSeedRunner>();
			dataSeedRunner.SeedData<CoreProfile>();
		}

		this.ServiceProvider = serviceProvider;
	}

	[TestCleanup]
	public virtual void TestCleanup()
	{
		scope.Dispose();
		if (this.ServiceProvider is IDisposable)
		{
			((IDisposable)this.ServiceProvider).Dispose();
		}
		this.ServiceProvider = null;
	}

	protected virtual IServiceCollection CreateServiceCollection()
	{
		IServiceCollection services = new ServiceCollection();
		services.ConfigureForTests(useInMemoryDb: !UseLocalDb);

		return services;
	}
}
