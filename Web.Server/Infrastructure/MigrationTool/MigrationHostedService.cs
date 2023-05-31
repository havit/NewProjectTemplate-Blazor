using Havit.AspNetCore.ExceptionMonitoring.Services;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using ProtoBuf.Meta;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.MigrationTool;

public class MigrationHostedService : IHostedService
{
	private readonly IServiceProvider serviceProvider;
	private readonly MigrationsOptions migrationsOptions;

	public MigrationHostedService(IServiceProvider serviceProvider, IOptions<MigrationsOptions> migrationsOptions)
	{
		this.serviceProvider = serviceProvider;
		this.migrationsOptions = migrationsOptions.Value;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		// https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service?pivots=dotnet-7-0
		// No scope is created for a hosted service by default.

		using var scope = serviceProvider.CreateScope();

		IMigrationService migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();

		// Preventivně už zde, abychom v případě problému s DI containarem zjistili problém dříve, než v případném catchi.
		IExceptionMonitoringService exceptionMonitoringService = scope.ServiceProvider.GetRequiredService<IExceptionMonitoringService>();

		if (migrationsOptions.RunMigrations)
		{
			try
			{
				migrationService.UpgradeDatabaseSchemaAndData();
			}
			catch (Exception exception)
			{
				exceptionMonitoringService.HandleException(exception);
				throw;
			}
		}

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
