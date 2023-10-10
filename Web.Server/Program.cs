using System.Runtime.InteropServices;
using Havit.NewProjectTemplate.DependencyInjection.Configuration;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.LoggingExtensions;

namespace Havit.NewProjectTemplate.Web.Server;

public static class Program
{
	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
			.ConfigureAppConfiguration((hostContext, config) =>
			{
				config
					.AddJsonFile("appsettings.WebServer.json", optional: false)
					.AddJsonFile($"appsettings.WebServer.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
#if DEBUG
					.AddJsonFile($"appsettings.WebServer.{hostContext.HostingEnvironment.EnvironmentName}.local.json", optional: true) // .gitignored
#endif
					.AddEnvironmentVariables()
					.AddCustomizedAzureKeyVault();
			})
			.ConfigureLogging((hostingContext, logging) =>
			{
				logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
				logging.AddConsole();
				logging.AddDebug();
				logging.AddCustomizedAzureWebAppDiagnostics();

				if (!hostingContext.HostingEnvironment.IsDevelopment() && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					logging.AddEventLog();
				}
			});
}
