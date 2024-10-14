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
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly IConfiguration _configuration;

	public MigrationService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
	{
		_serviceScopeFactory = serviceScopeFactory;
		_configuration = configuration;
	}

	public async Task UpgradeDatabaseSchemaAndDataAsync(CancellationToken cancellationToken = default)
	{
		using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
		{
			var context = serviceScope.ServiceProvider.GetRequiredService<IDbContext>();

			context.Database.SetCommandTimeout(TimeSpan.FromSeconds(_configuration.GetValue<int?>("AppSettings:Migrations:CommandTimeout") ?? 300));
			await context.Database.MigrateAsync(cancellationToken);

			var dataSeedRunner = serviceScope.ServiceProvider.GetRequiredService<IDataSeedRunner>();
			await dataSeedRunner.SeedDataAsync<CoreProfile>(false, cancellationToken);
		}
	}
}