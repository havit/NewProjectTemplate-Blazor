using Hangfire;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.SqlServer;
using Hangfire.States;
using Havit.ApplicationInsights.DependencyCollector;
using Havit.AspNetCore.ExceptionMonitoring.Services;
using Havit.Hangfire.Extensions.BackgroundJobs;
using Havit.Hangfire.Extensions.Filters;
using Havit.Hangfire.Extensions.RecurringJobs;
using Havit.Hangfire.Extensions.States;
using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.DependencyInjection.Configuration;
using Havit.NewProjectTemplate.JobsRunner.Infrastructure.ApplicationInsights;
using Havit.NewProjectTemplate.Services.Infrastructure.Logging;
using Havit.NewProjectTemplate.Services.Jobs;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.JobsRunner;

public static class Program
{
	public static async Task Main(string[] args)
	{
		bool useHangfire = args.Length == 0;

		var builder = Host.CreateApplicationBuilder();

		builder.Configuration.AddJsonFile("appsettings.JobsRunner.json", optional: false);
		builder.Configuration.AddJsonFile($"appsettings.JobsRunner.{builder.Environment.EnvironmentName}.json", optional: true);
#if DEBUG
		builder.Configuration.AddJsonFile($"appsettings.JobsRunner.{builder.Environment.EnvironmentName}.local.json", optional: true); // .gitignored
#endif
		builder.Configuration.AddEnvironmentVariables();
		builder.Configuration.AddCustomizedAzureKeyVault();

		builder.Logging.AddSimpleConsole(configure => configure.TimestampFormat = "[HH:mm:ss] ");
		builder.Logging.AddCustomizedAzureWebAppDiagnostics("JobsRunner");

		builder.Services.AddMemoryCache();

		builder.Services.AddApplicationInsightsTelemetryWorkerService();
		builder.Services.AddApplicationInsightsTelemetryProcessor<IgnoreSucceededDependenciesWithNoParentIdProcessor>();
		builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });
		builder.Services.Remove(builder.Services.Single(descriptor => descriptor.ImplementationType == typeof(PerformanceCollectorModule)));
		builder.Services.AddSingleton<ITelemetryInitializer, JobsRunnerToCloudRoleNameTelemetryInitializer>();

		builder.Services.AddExceptionMonitoring(builder.Configuration);

		builder.Services.ConfigureForJobsRunner(builder.Configuration);

		if (useHangfire)
		{
			builder.Services.AddHangfire((serviceProvider, configuration) => configuration
				.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
				.UseSimpleAssemblyNameTypeSerializer()
				.UseRecommendedSerializerSettings()
				.UseSqlServerStorage(() => new Microsoft.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("Database")), new SqlServerStorageOptions
				{
					CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
					SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
					QueuePollInterval = TimeSpan.FromSeconds(5),
					UseRecommendedIsolationLevel = true,
					DisableGlobalLocks = true,
					EnableHeavyMigrations = true
				})
				.WithJobExpirationTimeout(TimeSpan.FromDays(30)) // history
				.UseFilter(new FinalFailedStateFilter()) // Enable FinalFailedState, which ensures expiration of failed jobs as well
				.UseFilter(new AutomaticRetryAttribute { Attempts = 0 }) // Do not retry failed jobs
				.UseFilter(new ContinuationsSupportAttribute(new HashSet<string> { FinalFailedState.StateName, DeletedState.StateName, SucceededState.StateName })) // only working with AutomaticRetryAttribute with Attempts = 0
				.UseFilter(new CancelRecurringJobWhenAlreadyInQueueOrCurrentlyRunningFilter())
				.UseFilter(new DeleteSequenceRecurringJobSchedulerFilter()) // ensures the removal of system states of jobs that ensure the execution of recurring jobs in sequence
				.UseFilter(new ExceptionMonitoringAttribute(serviceProvider.GetRequiredService<IExceptionMonitoringService>()))
				.UseFilter(new ApplicationInsightAttribute(serviceProvider.GetRequiredService<TelemetryClient>()) { JobNameFunc = backgroundJob => Havit.Hangfire.Extensions.Helpers.JobNameHelper.TryGetSimpleName(backgroundJob.Job, out string simpleName) ? simpleName : backgroundJob.Job.ToString() })
				.UseConsole()
			);

			builder.Services.AddHangfireConsoleExtensions(); // adds support for Hangfire jobs logging  to a dashboard using ILogger<T> (.UseConsole() in hangfire configuration is required!)

#if DEBUG
			builder.Services.AddHangfireEnqueuedJobsCleanupOnApplicationStartup();
#endif
			builder.Services.AddHangfireRecurringJobsSchedulerOnApplicationStartup(GetRecurringJobsToSchedule().ToArray());

			// Add the processing server as IHostedService
			builder.Services.AddHangfireServer(o => o.WorkerCount = 1);
		}

		IHost host = builder.Build();

		if (useHangfire)
		{
			// Run with Hangfire
			using (WebJobsShutdownWatcher webJobsShutdownWatcher = new WebJobsShutdownWatcher())
			{
				await host.RunAsync(webJobsShutdownWatcher.Token);
			}
		}
		else
		{
			// Run with command line
			if ((args.Length > 1) || (!await TryRunCommandAsync(host.Services, args[0])))
			{
				ShowCommandsHelp();
			}
		}
	}

	private static IEnumerable<IRecurringJob> GetRecurringJobsToSchedule()
	{
		TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

		yield return new RecurringJob<IEmptyJob>(job => job.ExecuteAsync(CancellationToken.None), Cron.Minutely(), timeZone);
	}

	private static async Task<bool> TryRunCommandAsync(IServiceProvider serviceProvider, string command)
	{
		Contract.Requires<ArgumentNullException>(serviceProvider != null);
		Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(command));

		var job = GetRecurringJobsToSchedule().SingleOrDefault(job => String.Equals(job.JobId, command, StringComparison.CurrentCultureIgnoreCase));
		if (job == null)
		{
			return false;
		}

		using (var scopeService = serviceProvider.CreateScope())
		{
			IExceptionMonitoringService exceptionMonitoringService = serviceProvider.GetRequiredService<IExceptionMonitoringService>();
			try
			{
				await job.RunAsync(scopeService.ServiceProvider, CancellationToken.None);
			}
			catch (Exception ex)
			{
				exceptionMonitoringService.HandleException(ex);

				throw;
			}
		}

		return true;
	}

	private static void ShowCommandsHelp()
	{
		Console.WriteLine("Supported commands:");
		foreach (var job in GetRecurringJobsToSchedule().OrderBy(job => job.JobId).ToList())
		{
			Console.WriteLine("  " + job.JobId);
		}
	}
}
