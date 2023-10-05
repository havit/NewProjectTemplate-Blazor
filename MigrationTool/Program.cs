using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.MigrationTool;

public static class Program
{
	public static async Task Main(string[] args)
	{
		IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
			.ConfigureAppConfiguration((hostContext, config) =>
			{
				config
					.AddCommandLine(args, new Dictionary<string, string> { { "--connectionstring", "ConnectionStrings:Database" }, { "--commandtimeout", "AppSettings:Migrations:CommandTimeout" } })
					.AddEnvironmentVariables();
			})
			.ConfigureLogging(logging =>
			{
				logging.AddSimpleConsole(configure => configure.TimestampFormat = "[HH:mm:ss] ");
			})
			.ConfigureServices((hostContext, services) =>
			{
				services.ConfigureForMigrationTool(hostContext.Configuration);
			});

		await hostBuilder.Build().Services.GetRequiredService<IMigrationService>().UpgradeDatabaseSchemaAndDataAsync();
	}
}