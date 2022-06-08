using Hangfire;
using Hangfire.Dashboard;
using Havit.Blazor.Grpc.Server;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Services.HealthChecks;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.ApplicationInsights;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.HealthChecks;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;

namespace Havit.NewProjectTemplate.Web.Server;

public class Startup
{
	private readonly IConfiguration configuration;

	public Startup(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.ConfigureForWebServer(configuration);

		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		services.AddDatabaseDeveloperPageExceptionFilter();

		services.AddOptions();

		services.AddCustomizedMailing(configuration);

		// SmtpExceptionMonitoring to errors@havit.cz
		services.AddExceptionMonitoring(configuration);

		// Application Insights
		services.AddApplicationInsightsTelemetry(configuration);
		services.AddSingleton<ITelemetryInitializer, GrpcRequestStatusTelemetryInitializer>();
		services.AddSingleton<ITelemetryInitializer, EnrichmentTelemetryInitializer>();
		services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });

		services.AddAuthorization(options =>
		{
			options.AddPolicy(PolicyNames.HangfireDashboardAcccessPolicy, policy => policy
				.RequireAuthenticatedUser()
				.RequireRole(nameof(Role.Entry.SystemAdministrator)));
		});
		services.AddCustomizedAuth(configuration);

		// server-side UI
		services.AddControllersWithViews();
		services.AddRazorPages();

		// gRPC
		services.AddGrpcServerInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);
		services.AddCodeFirstGrpcReflection();

		// Health checks
		services.AddHealthChecks()
			.AddCheck<NewProjectTemplateDbContextHealthCheck>("Database");

		// Hangfire
		services.AddCustomizedHangfire(configuration);
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseMigrationsEndPoint();
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			// TODO app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();

		app.UseExceptionMonitoring();

		app.UseRouting();

		app.UseAuthentication();
		app.UseIdentityServer();
		app.UseAuthorization();

		app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapRazorPages();
			endpoints.MapControllers();
			endpoints.MapFallbackToPage("/_Host");

			endpoints.MapGrpcServicesByApiContractAttributes(
				typeof(IDataSeedFacade).Assembly,
				configureEndpointWithAuthorization: endpoint =>
				{
					endpoint.RequireAuthorization(); // TODO? AuthorizationPolicyNames.ApiScopePolicy when needed
				});
			endpoints.MapCodeFirstGrpcReflectionService();

			endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
			{
				AllowCachingResponses = false,
				ResponseWriter = HealthCheckWriter.WriteResponse
			});

			endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
			{
				Authorization = new List<IDashboardAuthorizationFilter>() { }, // see https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/
				DisplayStorageConnectionString = false,
				DashboardTitle = "NewProjectTemplate - Jobs",
				StatsPollingInterval = 60_000, // once a minute
				DisplayNameFunc = (_, job) => Havit.Hangfire.Extensions.Helpers.JobNameHelper.TryGetSimpleName(job, out string simpleName)
													? simpleName
													: job.ToString()
			})
			.RequireAuthorization(PolicyNames.HangfireDashboardAcccessPolicy);
		});

		if (configuration.GetValue<bool>("AppSettings:Migrations:RunMigrations"))
		{
			app.ApplicationServices.GetRequiredService<IMigrationService>().UpgradeDatabaseSchemaAndData();
		}
	}
}
