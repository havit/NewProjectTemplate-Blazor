using System.Globalization;
using System.Security.Claims;
using BlazorApplicationInsights;
using Blazored.LocalStorage;
using FluentValidation;
using Havit.Blazor.Grpc.Client;
using Havit.Blazor.Grpc.Client.ServerExceptions;
using Havit.Blazor.Grpc.Client.WebAssembly;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Havit.NewProjectTemplate.Web.Client;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.RootComponents.Add<App>("app");

		AddLoggingAndApplicationInsights(builder);
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

		await SetLanguage(webAssemblyHost);

		await webAssemblyHost.RunAsync();
	}
	private static void SetHxComponents()
	{
		HxOffcanvas.Defaults.Backdrop = OffcanvasBackdrop.Static;
		HxModal.Defaults.Backdrop = ModalBackdrop.Static;
		HxInputDate.Defaults.CalendarIcon = BootstrapIcon.Calendar3;

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
		builder.Services.AddScoped<IUserClaimsRetrievalService, UserClaimsRetrievalService>();
		builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(CustomAccountClaimsPrincipalFactory));

		builder.Services.AddHttpClient("Web.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
			.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
		builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Web.Server"));

		builder.Services
			.AddMsalAuthentication(options =>
			{
				builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
				options.UserOptions.RoleClaim = ClaimTypes.Role;
				options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration["Auth:WebServerScope"]);
				options.ProviderOptions.LoginMode = "redirect";
			})
			.AddAccountClaimsPrincipalFactory<CustomAccountClaimsPrincipalFactory>();

		//builder.Services.Configure<AuthorizationOptions>(config =>
		//{
		//	config.AddPolicy(...);
		//});

		// UserClientService uses Web.Server named HttpClient via IHttpClientFactory to break the dependency cycle
		// https://github.com/dotnet/aspnetcore/issues/33787
		// https://stackoverflow.com/questions/70935768/call-api-from-accountclaimsprincipalfactory-in-blazor-wasm
		builder.Services.AddScoped<IUserClaimsRetrievalService, UserClaimsRetrievalService>();
	}

	private static void AddGrpcClient(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddTransient<IOperationFailedExceptionGrpcClientListener, HxMessengerOperationFailedExceptionGrpcClientListener>();
		builder.Services.AddTransient<AuthorizationGrpcClientInterceptor>();

		builder.Services.AddGrpcClientInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);

		builder.Services.AddGrpcClientsByApiContractAttributes(
			typeof(IDataSeedFacade).Assembly,
			configureGrpcClientWithAuthorization: grpcClient =>
			{
				grpcClient
					.AddHttpMessageHandler(provider =>
					{
						return provider.GetRequiredService<AuthorizationMessageHandler>()
							.ConfigureHandler(
								authorizedUrls: new[] { builder.HostEnvironment.BaseAddress },
								scopes: new[] { builder.Configuration["Auth:WebServerScope"] }
							);
					})
					.AddInterceptor<AuthorizationGrpcClientInterceptor>();
			});
	}

	private static async ValueTask SetLanguage(WebAssemblyHost webAssemblyHost)
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

	private static void AddLoggingAndApplicationInsights(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddBlazorApplicationInsights();

		builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(level => (level == LogLevel.Error) || (level == LogLevel.Critical));

#if DEBUG
		builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
	}
}
