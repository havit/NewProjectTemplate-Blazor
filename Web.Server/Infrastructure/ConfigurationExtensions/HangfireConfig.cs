using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using Havit.Hangfire.Extensions.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public static class HangfireConfig
{
	public static void AddCustomizedHangfire(this IServiceCollection services, IConfiguration configuration)
	{
		string connectionString = configuration.GetConnectionString("Database");

		services.AddHangfire(configuration =>
		{
			configuration.UseSimpleAssemblyNameTypeSerializer();
			configuration.UseRecommendedSerializerSettings();
			configuration.UseFilter(new CancelRecurringJobWhenAlreadyInQueueOrCurrentlyRunningFilter());
			configuration.UseSqlServerStorage(() => new Microsoft.Data.SqlClient.SqlConnection(connectionString), new SqlServerStorageOptions
			{
				// hangfire recommended configuration for hangfire 1.7
				CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
				SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
				QueuePollInterval = TimeSpan.FromSeconds(5),
				UseRecommendedIsolationLevel = true,
				DisableGlobalLocks = true // Migration to Schema 7 is required
			});
			configuration.UseConsole(); // shows "processing log" in hangfire dashboard
			configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
		});
	}
}
