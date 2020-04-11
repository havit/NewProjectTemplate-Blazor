using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;

namespace Havit.GoranG3.WebAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory); // Protože in-process čte statické files z jiného místa
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
#if DEBUG
					webBuilder.UseEnvironment("Development"); // pro Red-Gate ANTS Performance Profiler
					webBuilder.UseUrls("http://localhost:9901"); // pro Red-Gate ANTS Performance Profiler
#endif
				})
				.ConfigureAppConfiguration((hostContext, config) =>
				{
					// delete all default configuration providers
					config.Sources.Clear();
					config
						.AddJsonFile("appsettings.WebAPI.json", optional: false)
						.AddJsonFile($"appsettings.WebAPI.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
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
