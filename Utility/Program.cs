using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.SqlServer;
using Havit.AspNetCore.ExceptionMonitoring.Services;
using Havit.Diagnostics.Contracts;
using Havit.Hangfire.Extensions.BackgroundJobs;
using Havit.Hangfire.Extensions.Filters;
using Havit.Hangfire.Extensions.RecurringJobs;
using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.Services.Jobs;
using Havit.UverovaPlatforma.Utility.Hangfire;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Havit.UverovaPlatforma.Utility
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			if (args.Length > 0)
			{
				string command = args[0];

				Console.WriteLine($"Command: {command}");

				bool successfullyCompleted =
					await TryDoCommand<IEmptyJob>(command, "EmptyJob");

				if (!successfullyCompleted)
				{
					Console.WriteLine("Nepodařilo se zpracovat příkaz: {0}", command);
					Console.WriteLine();

					ShowCommandsHelp();
				}
			}
			else
			{
				await RunHangfireServer();
			}
		}

		private static void ShowCommandsHelp()
		{
			Console.WriteLine("Podporované příkazy jsou:");
			Console.WriteLine("  EmptyJob");
		}

		private static async Task<bool> TryDoCommand<TJob>(string command, string commandPattern)
			where TJob : class, IRunnableJob
		{
			Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(command));
			Contract.Requires<ArgumentNullException>(commandPattern != null);

			if (!String.Equals(command, commandPattern, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			await ExecuteWithServiceProvider(async serviceProvider =>
			{
				try
				{
					using (var scopeService = serviceProvider.CreateScope())
					{
						TJob job = scopeService.ServiceProvider.GetRequiredService<TJob>();
						await job.ExecuteAsync(CancellationToken.None);
					}
				}
				catch (Exception ex)
				{
					var service = serviceProvider.GetRequiredService<IExceptionMonitoringService>();
					service.HandleException(ex);

					throw;
				}
			});

			return true;
		}

		private static IEnumerable<IRecurringJob> GetRecurringJobsToSchedule()
		{
			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

			yield return new RecurringJob<EmptyJob>(job => job.ExecuteAsync(CancellationToken.None), Cron.Daily(hour: 1, minute: 0), timeZone);
		}

		private static async Task RunHangfireServer()
		{
			string connectionString = Configuration.Value.GetConnectionString("Database");

			await ExecuteWithServiceProvider(async (serviceProvider) =>
			{
				GlobalConfiguration.Configuration
					.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
					.UseSimpleAssemblyNameTypeSerializer()
					.UseFilter(new AutomaticRetryAttribute { Attempts = 0 }) // do not retry failed jobs
					.UseFilter(new ApplicationInsightAttribute(serviceProvider.GetRequiredService<TelemetryClient>()))
					.UseFilter(new ExceptionMonitoringAttribute(serviceProvider))
					.UseFilter(new CancelRecurringJobWhenAlreadyInQueueOrCurrentlyRunningFilter())
					.UseActivator(new AspNetCoreJobActivator(serviceProvider.GetRequiredService<IServiceScopeFactory>()))
					.UseSqlServerStorage(() => new Microsoft.Data.SqlClient.SqlConnection(connectionString), new SqlServerStorageOptions
					{
						CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
						SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
						QueuePollInterval = TimeSpan.Zero,
						UseRecommendedIsolationLevel = true,
						DisableGlobalLocks = true // Migration to Schema 7 is required
					})
					.UseLogProvider(new AspNetCoreLogProvider(serviceProvider.GetRequiredService<ILoggerFactory>())) // enables .NET Core logging for hangfire server (not jobs!) https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/
					.UseConsole(); // enables logging jobs progress to hangfire dashboard (only using a PerformContext class; for ILogger<> support add services.AddHangfireConsoleExtensions())
				JobStorage.Current.JobExpirationTimeout = TimeSpan.FromDays(30); // keep history

#if DEBUG
				BackgroundJobHelper.DeleteEnqueuedJobs();
#endif

				// schedule recurring jobs
				RecurringJobsHelper.SetSchedule(GetRecurringJobsToSchedule().ToArray());

				var options = new BackgroundJobServerOptions
				{
					WorkerCount = 1
				};

				using (var server = new BackgroundJobServer(options))
				{
					if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")))
					{
						// running in Azure
						Console.WriteLine("Hangfire Server started. Waiting for shutdown signal...");
						using (WebJobsShutdownWatcher webJobsShutdownWatcher = new WebJobsShutdownWatcher())
						{
							await server.WaitForShutdownAsync(webJobsShutdownWatcher.Token);
						}
					}
					else
					{
						// running outside of Azure
						Console.WriteLine("Hangfire Server started. Press Enter to exit...");
						Console.ReadLine();
					}
				}
			});
		}

		private static async Task ExecuteWithServiceProvider(Func<IServiceProvider, Task> action)
		{
			IConfiguration configuration = Configuration.Value;

			// Setup ServiceCollection
			IServiceCollection services = new ServiceCollection();

			services.AddOptions();
			services.AddMemoryCache();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddLogging(builder => builder.AddSimpleConsole(options => options.TimestampFormat = "[HH:mm:ss] "));
			services.AddHangfireConsoleExtensions(); // adds support for Hangfire jobs logging  to a dashboard using ILogger<T> (.UseConsole() in hangfire configuration is required!)
			services.AddExceptionMonitoring(configuration);

			// TODO: Should this be in the template?
			//services.AddOptions<ApiCommunicationLogStorageOptions>().Bind(configuration.GetSection("AppSettings:ApiCommunicationLogStorage"));

			services.ConfigureForUtility(configuration);

			string sourceDbConnectionString = configuration.GetConnectionString("Database");

			services.AddSingleton<TelemetryClient>();

			using (ServiceProvider serviceProvider = services.BuildServiceProvider())
			{
				await action(serviceProvider);
			}
		}

		private static Lazy<IConfiguration> Configuration = new Lazy<IConfiguration>(() =>
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

			var configurationBuilder = new ConfigurationBuilder()
				.AddJsonFile(@"appsettings.Utility.json", optional: false)
				.AddJsonFile($"appsettings.Utility.{environmentName}.json", optional: true)
				.AddEnvironmentVariables();

			return configurationBuilder.Build();
		});
	}
}
