using Havit.AspNetCore.ExceptionMonitoring.Services;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.MigrationTool;

public class MigrationHostedService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly MigrationsOptions _migrationsOptions;

	public MigrationHostedService(IServiceProvider serviceProvider, IOptions<MigrationsOptions> migrationsOptions)
	{
		_serviceProvider = serviceProvider;
		_migrationsOptions = migrationsOptions.Value;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		// https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service?pivots=dotnet-7-0
		// No scope is created for a hosted service by default.

		using var scope = _serviceProvider.CreateScope();

		IMigrationService migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();

		// Preemptively placed here to detect any issues with the DI container before the catch block, if any.
		IExceptionMonitoringService exceptionMonitoringService = scope.ServiceProvider.GetRequiredService<IExceptionMonitoringService>();

		if (_migrationsOptions.RunMigrations)
		{
			try
			{
				await migrationService.UpgradeDatabaseSchemaAndDataAsync(cancellationToken);
			}
			catch (Exception exception)
			{
				exceptionMonitoringService.HandleException(exception);
				throw;
			}
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
