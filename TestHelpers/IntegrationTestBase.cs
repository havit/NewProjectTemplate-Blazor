using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Havit.NewProjectTemplate.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Havit.NewProjectTemplate.TestHelpers;

public class IntegrationTestBase
{
	protected IServiceProvider ServiceProvider { get; private set; }

	protected virtual bool UseLocalDb => false;
	protected virtual bool DeleteDbData => true;

	protected virtual bool SeedData => true;

	private ServiceProvider serviceProvider;

	[TestInitialize]
	public virtual void TestInitialize()
	{
		IServiceCollection services = CreateServiceCollection();
		serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
		{
			ValidateOnBuild = true,
			ValidateScopes = true
		});

		using (var scope = serviceProvider.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
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
				var dataSeedRunner = scope.ServiceProvider.GetRequiredService<IDataSeedRunner>();
				dataSeedRunner.SeedData<CoreProfile>();
			}
		}

		this.ServiceProvider = serviceProvider.CreateScope().ServiceProvider;
	}

	[TestCleanup]
	public virtual void TestCleanup()
	{
		((IDisposable)ServiceProvider)?.Dispose();
		serviceProvider?.Dispose();
	}

	protected virtual IServiceCollection CreateServiceCollection()
	{
		IServiceCollection services = new ServiceCollection();
		services.ConfigureForTests(useInMemoryDb: !UseLocalDb);

		return services;
	}
}
