using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;

[Service]
public class MigrationService : IMigrationService
{
	private readonly IServiceScopeFactory serviceScopeFactory;
	private readonly IConfiguration configuration;

	public MigrationService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
	{
		this.serviceScopeFactory = serviceScopeFactory;
		this.configuration = configuration;
	}

	public void UpgradeDatabaseSchemaAndData()
	{
		using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
		{
			var context = serviceScope.ServiceProvider.GetService<IDbContext>();

			context.Database.SetCommandTimeout(TimeSpan.FromSeconds(configuration.GetValue<int?>("AppSettings:Migrations:CommandTimeout") ?? 300));
			context.Database.Migrate();

			var dataSeedRunner = serviceScope.ServiceProvider.GetService<IDataSeedRunner>();
			dataSeedRunner.SeedData<CoreProfile>();
		}
	}
}