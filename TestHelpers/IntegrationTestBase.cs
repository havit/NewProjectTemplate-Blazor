using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Havit.NewProjectTemplate.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.NewProjectTemplate.TestHelpers;

public class IntegrationTestBase
{
	protected IServiceProvider ServiceProvider { get; private set; }

	protected virtual bool UseLocalDb => false;
	protected virtual bool DeleteDbData => true;

	protected virtual bool SeedData => true;

	private ServiceProvider serviceProvider;
	private IConfiguration configuration;

	public IConfiguration Configuration
	{
		get
		{
			if (configuration == null)
			{
				var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
				configuration = builder.Build();
			}

			return configuration;
		}
	}

	[TestInitialize]
	public virtual async Task TestInitialize()
	{
		IServiceCollection services = new ServiceCollection();
		ConfigureServices(services, this.Configuration);

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
				await dbContext.Database.EnsureDeletedAsync();
			}
			if (this.UseLocalDb)
			{
				await dbContext.Database.MigrateAsync();
			}

			if (this.SeedData)
			{
				var dataSeedRunner = scope.ServiceProvider.GetRequiredService<IDataSeedRunner>();
				await dataSeedRunner.SeedDataAsync<CoreProfile>();
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

	protected virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigureForTests(useInMemoryDb: !UseLocalDb);
	}
}