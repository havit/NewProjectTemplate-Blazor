using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Havit.GoranG3.Web
{
	public static class Program
	{
		public static void Main(string[] args)
		{
            Directory.SetCurrentDirectory(AppContext.BaseDirectory); // Protože in-process čte statické files z jiného místa
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateWebHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
#if DEBUG
					webBuilder.UseEnvironment("Development"); // pro pohodlnější spuštění z command line
					webBuilder.UseUrls("http://localhost:9900"); // pro pohodlnější spuštění z command line
#endif
				})
				.ConfigureAppConfiguration((hostContext, config) =>
				{
					// delete all default configuration providers
					config.Sources.Clear();
					config
						.AddJsonFile("appsettings.Web.json", optional: false)
						.AddJsonFile($"appsettings.Web.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
						.AddEnvironmentVariables();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
#if !DEBUG
					logging.AddEventLog();
#endif
				});
		}
	}
}
