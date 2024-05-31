using System.Globalization;
using BlazorApplicationInsights;
using BlazorApplicationInsights.Models;
using Blazored.LocalStorage;
using FluentValidation;
using Havit.Blazor.Grpc.Client;
using Havit.Blazor.Grpc.Client.ServerExceptions;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Havit.NewProjectTemplate.Web.Client;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		// We don't have the Web.Client/wwwroot/appsettings.(...).json file on disk, so it is not listed in blazor.boot.json.
		// As a result, Blazor will automatically request the configuration from the server, which we need to handle "manually"
		// by adding the download of the configuration from the expected endpoint, see:
		// https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/configuration?view=aspnetcore-8.0#app-settings-configuration
		builder = await builder.AddJsonStreamAsync(WebClientOptions.WebClientConfigurationRoute);

		AddLoggingAndBlazorApplicationInsights(builder);
		AddAuthWithHttpClient(builder);

		builder.Services.AddBlazoredLocalStorage();
		builder.Services.AddValidatorsFromAssemblyContaining<Dto<object>>();

		builder.Services.AddHxServices();
		builder.Services.AddHxMessenger();
		builder.Services.AddHxMessageBoxHost();
		Havit.NewProjectTemplate.Web.Client.Resources.ResourcesServiceCollectionInstaller.AddGeneratedResourceWrappers(builder.Services);
		Havit.NewProjectTemplate.Resources.ResourcesServiceCollectionInstaller.AddGeneratedResourceWrappers(builder.Services);
		SetHxComponents();

		AddGrpcClient(builder);

		WebAssemblyHost webAssemblyHost = builder.Build();

		await SetLanguageAsync(webAssemblyHost);

		await webAssemblyHost.RunAsync();
	}
	private static void SetHxComponents()
	{
		HxOffcanvas.Defaults.Backdrop = OffcanvasBackdrop.Static;
		HxModal.Defaults.Backdrop = ModalBackdrop.Static;
		HxInputDate.Defaults.CalendarIcon = BootstrapIcon.Calendar3;
		HxInputDateRange.Defaults.CalendarIcon = BootstrapIcon.Calendar3;

		// TODO [OPTIONAL] Setup HxInputDateRange.Defaults.PredefinedRanges here
		//DateTime today = DateTime.Today;
		//DateTime thisMonthStart = new DateTime(today.Year, today.Month, 1);
		//DateTime thisMonthEnd = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
		//DateTime thisYearStart = new DateTime(today.Year, 1, 1);
		//DateTime thisYearEnd = new DateTime(today.Year, 12, 31);

		//HxInputDateRange.Defaults.PredefinedDateRanges = new InputDateRangePredefinedRangesItem[]
		//{
		//	new() { Label = "TTM", DateRange = new DateTimeRange(today.AddMonths(-12).AddDays(1), today) },
		//	new() { Label = "ThisYear", DateRange = new DateTimeRange(thisYearStart, thisYearEnd), ResourceType = typeof(HxInputDateRangePredefinedRanges) },
		//	new() { Label = "ThisMonth", DateRange = new DateTimeRange(thisMonthStart, thisMonthEnd), ResourceType = typeof(HxInputDateRangePredefinedRanges) },
		//};
	}

	public static void AddAuthWithHttpClient(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddHttpClient("Web.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
		builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Web.Server"));

		builder.Services.AddAuthorizationCore();
		builder.Services.AddCascadingAuthenticationState();
		builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

		//builder.Services.Configure<AuthorizationOptions>(config =>
		//{
		//	config.AddPolicy(...);
		//});
	}

	private static void AddGrpcClient(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddTransient<IOperationFailedExceptionGrpcClientListener, HxMessengerOperationFailedExceptionGrpcClientListener>();

		builder.Services.AddGrpcClientInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);

		builder.Services.AddGrpcClientsByApiContractAttributes(typeof(IDataSeedFacade).Assembly);
	}

	private static async ValueTask SetLanguageAsync(WebAssemblyHost webAssemblyHost)
	{
		var localStorageService = webAssemblyHost.Services.GetService<ILocalStorageService>();

		var culture = await localStorageService.GetItemAsStringAsync("culture");
		if (!String.IsNullOrWhiteSpace(culture))
		{
			var cultureInfo = new CultureInfo(culture);
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
		}
	}

	private static void AddLoggingAndBlazorApplicationInsights(WebAssemblyHostBuilder builder)
	{
		ApplicationInsightsOptions applicationInsightsOptions = builder.Configuration.GetSection(ApplicationInsightsOptions.Path).Get<ApplicationInsightsOptions>();

		builder.Services.AddBlazorApplicationInsights(
			c => c.ConnectionString = applicationInsightsOptions.ConnectionString ?? String.Empty,
			async applicationInsights =>
			{
				var telemetryItem = new TelemetryItem()
				{
					Tags = new Dictionary<string, object>()
					{
						{ "ai.cloud.role", "Web.Client" },
					}
				};

				await applicationInsights.AddTelemetryInitializer(telemetryItem);
			});

		builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(level => (level == LogLevel.Error) || (level == LogLevel.Critical));

#if DEBUG
		builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
	}
}
