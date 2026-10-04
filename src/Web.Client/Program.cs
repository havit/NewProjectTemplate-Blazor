using System.Globalization;
using FluentValidation;
using Havit.Blazor.ApplicationInsights;
using Havit.Blazor.ApplicationInsights.Logging;
using Havit.Blazor.Grpc.Client;
using Havit.Blazor.Storage;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Havit.NewProjectTemplate.Web.Client;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		// We don't have the Web.Client/wwwroot/appsettings.(...).json file on disk, so it is not included in the boot configuration.
		// As a result, Blazor will automatically request the configuration from the server, which we need to handle "manually"
		// by adding the download of the configuration from the expected endpoint, see:
		// https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/configuration?view=aspnetcore-10.0#app-settings-configuration
		builder = await builder.AddJsonStreamAsync(WebClientOptions.WebClientConfigurationRoute);

		AddLoggingAndBlazorApplicationInsights(builder);
		AddAuthWithHttpClient(builder);

		builder.Services.AddHavitBlazorStorage();
		builder.Services.AddValidatorsFromAssemblyContaining<Dto<object>>();
		builder.Services.AddClientByServiceAttribute();

		builder.Services.AddHxServices();
		builder.Services.AddHxMessenger();
		builder.Services.AddHxMessageBoxHost();
		Havit.NewProjectTemplate.Web.Client.ResourcesServiceCollectionExtensions.AddGeneratedResourceWrappers(builder.Services);
		Havit.NewProjectTemplate.Resources.ResourcesServiceCollectionExtensions.AddGeneratedResourceWrappers(builder.Services);
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
		// DateTime today = DateTime.Today;
		// DateTime thisMonthStart = new DateTime(today.Year, today.Month, 1);
		// DateTime thisMonthEnd = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
		// DateTime thisYearStart = new DateTime(today.Year, 1, 1);
		// DateTime thisYearEnd = new DateTime(today.Year, 12, 31);

		// HxInputDateRange.Defaults.PredefinedDateRanges = new InputDateRangePredefinedRangesItem[]
		// {
		// 	new() { Label = "TTM", DateRange = new DateTimeRange(today.AddMonths(-12).AddDays(1), today) },
		// 	new() { Label = "ThisYear", DateRange = new DateTimeRange(thisYearStart, thisYearEnd), ResourceType = typeof(HxInputDateRangePredefinedRanges) },
		// 	new() { Label = "ThisMonth", DateRange = new DateTimeRange(thisMonthStart, thisMonthEnd), ResourceType = typeof(HxInputDateRangePredefinedRanges) },
		// };
	}

	public static void AddAuthWithHttpClient(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddHttpClient("Web.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
		builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Web.Server"));

		builder.Services.AddAuthorizationCore();
		builder.Services.AddCascadingAuthenticationState();
		builder.Services.AddAuthenticationStateDeserialization();

		// builder.Services.Configure<AuthorizationOptions>(config =>
		// {
		//     config.AddPolicy(...);
		// });
	}

	private static void AddGrpcClient(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddGrpcClientInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);

		builder.Services.AddGrpcClientsByApiContractAttributes(typeof(IDataSeedFacade).Assembly);
	}

	private static async ValueTask SetLanguageAsync(WebAssemblyHost webAssemblyHost)
	{
		var localStorageService = webAssemblyHost.Services.GetService<ILocalStorageService>();

		var (cultureFound, culture) = await localStorageService.TryGetStringValueAsync("culture");
		if (cultureFound && !String.IsNullOrWhiteSpace(culture))
		{
			var cultureInfo = new CultureInfo(culture);
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
		}
	}

	private static void AddLoggingAndBlazorApplicationInsights(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddBlazorApplicationInsights();
		builder.Logging.AddBlazorApplicationInsights();
		builder.Logging.AddFilter<BlazorApplicationInsightsLoggerProvider>(level => (level == LogLevel.Error) || (level == LogLevel.Critical));

#if DEBUG
		builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
	}
}
