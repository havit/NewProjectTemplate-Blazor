using BlazorApplicationInsights;
using Hangfire;
using Hangfire.Dashboard;
using Havit.Blazor.Grpc.Server;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.DependencyInjection;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security;
using Havit.NewProjectTemplate.Primitives.Security;
using Havit.NewProjectTemplate.Services.HealthChecks;
using Havit.NewProjectTemplate.Services.Infrastructure.Security;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.ApplicationInsights;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.ExceptionHandling;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.HealthChecks;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.MigrationTool;
using Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using ProtoBuf.Grpc.Server;

namespace Havit.NewProjectTemplate.Web.Server;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddOptions();
		services.Configure<WebClientOptions>(_configuration.GetSection("WebClient"));

		services.ConfigureForWebServer(_configuration);

		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		services.AddDatabaseDeveloperPageExceptionFilter();

		services.AddCustomizedMailing(_configuration);

		// SmtpExceptionMonitoring to errors@havit.cz
		services.AddExceptionMonitoring(_configuration);

		// Application Insights
		services.AddApplicationInsightsTelemetry(_configuration);
		services.AddSingleton<ITelemetryInitializer, GrpcRequestStatusTelemetryInitializer>();
		services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
		services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });

		// The configuration is transferred to the client through prerendering
		services.AddBlazorApplicationInsights(c => c.ConnectionString = _configuration.GetSection("ApplicationInsights").GetValue<string>("ConnectionString"));

		// Authentication & Authorization
		services.AddCustomAuthentication(_configuration);
		services.AddAuthorizationBuilder()
			.AddPolicy(PolicyNames.HangfireDashboardAccessPolicy, policy => policy
					.AddAuthenticationSchemes(AuthenticationConfigurationExtension.MsOidcScheme)
					.RequireAuthenticatedUser()
					.RequireRole(nameof(RoleEntry.SystemAdministrator)));

		services.AddScoped<AuthenticationStateProvider, PersistingAuthenticationStateProvider>();
		services.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();

		// Blazor components
		services.AddRazorComponents()
			.AddInteractiveWebAssemblyComponents();

		// server-side UI
		services.AddRazorPages();
		services.AddControllersWithViews();

		// gRPC
		services.AddGrpcServerInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);
		services.AddCodeFirstGrpcReflection();

		// Health checks
		TimeSpan defaultHealthCheckTimeout = TimeSpan.FromSeconds(10);
		services.AddHealthChecks()
			.AddCheck<NewProjectTemplateDbContextHealthCheck>("Database", timeout: defaultHealthCheckTimeout)
			.AddCheck<MailServiceHealthCheck>("SMTP", timeout: defaultHealthCheckTimeout);

		// Hangfire
		services.AddCustomizedHangfire(_configuration);

		// Migrations
		services.Configure<MigrationsOptions>(_configuration.GetSection(MigrationsOptions.Path));
		services.AddHostedService<MigrationHostedService>();
	}

	public void ConfigureMiddleware(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseMigrationsEndPoint();
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			// TODO app.UseHsts();
		}

		app.UseMiddleware<CustomResponseForKnownExceptionsMiddleware>();

		app.UseHttpsRedirection();
		app.MapStaticAssets();
		//app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAntiforgery();

		app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });
	}

	public void ConfigureEndpoints(WebApplication app)
	{
		app.MapRazorPages();
		app.MapControllers();
		app.MapRazorComponents<App>()
			.AddInteractiveWebAssemblyRenderMode()
			.AddAdditionalAssemblies(typeof(Havit.NewProjectTemplate.Web.Client.Program).Assembly);

		app.MapGrpcServicesByApiContractAttributes(
				typeof(IDataSeedFacade).Assembly,
				configureEndpointWithAuthorization: endpoint =>
				{
					endpoint.RequireAuthorization(); // TODO? AuthorizationPolicyNames.ApiScopePolicy when needed
				});
		app.MapCodeFirstGrpcReflectionService();

		app.MapGroup("/authentication").MapLoginAndLogout();

		app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
		{
			AllowCachingResponses = false,
			ResponseWriter = HealthCheckWriter.WriteResponseAsync
		});

		app.MapHangfireDashboard("/hangfire", new DashboardOptions
		{
			DefaultRecordsPerPage = 50,
			Authorization = new List<IDashboardAuthorizationFilter>(), // see https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/
			DisplayStorageConnectionString = false,
			DashboardTitle = "NewProjectTemplate - Jobs",
			StatsPollingInterval = 60_000, // once a minute
			DisplayNameFunc = (_, job) => Havit.Hangfire.Extensions.Helpers.JobNameHelper.TryGetSimpleName(job, out string simpleName)
												? simpleName
												: job.ToString()
		})
		.RequireAuthorization(PolicyNames.HangfireDashboardAccessPolicy);
	}
}
