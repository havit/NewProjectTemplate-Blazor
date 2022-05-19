using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.MigrationTool;

public class Program
{
	public static void Main(string[] args)
	{
		IHostBuilder hostBuidler = Host.CreateDefaultBuilder()
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

		hostBuidler.Build().Services.GetRequiredService<IMigrationService>().UpgradeDatabaseSchemaAndData();
	}
}