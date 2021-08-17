using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorApplicationInsights;
using Blazored.LocalStorage;
using FluentValidation;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.Blazor.Grpc.Client;
using Havit.Blazor.Grpc.Client.ServerExceptions;
using Havit.Blazor.Grpc.Client.WebAssembly;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.System;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;
using Havit.NewProjectTemplate.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.Web.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			AddLoggingAndApplicationInsights(builder);

			builder.RootComponents.Add<App>("app");

			builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory)); // multiple roles workaround
			builder.Services.AddApiAuthorization();

			builder.Services.AddLocalization();
			Havit.NewProjectTemplate.Web.Client.Resources.ResourcesServiceCollectionInstaller.AddGeneratedResourceWrappers(builder.Services);
			Havit.NewProjectTemplate.Resources.ResourcesServiceCollectionInstaller.AddGeneratedResourceWrappers(builder.Services);

			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddValidatorsFromAssemblyContaining<Dto<object>>();

			builder.Services.AddHxMessenger();
			builder.Services.AddHxMessageBoxHost();
			SetHxComponents();

			builder.Services.AddScoped<IContactReferenceDataStore, ContactReferenceDataStore>();

			AddGrpcClient(builder);

			WebAssemblyHost webAssemblyHost = builder.Build();

			await SetLanguage(webAssemblyHost);

			await webAssemblyHost.RunAsync();
		}
		private static void SetHxComponents()
		{
			// HxProgressIndicator.DefaultDelay = 0;
		}

		private static void AddGrpcClient(WebAssemblyHostBuilder builder)
		{
			builder.Services.AddTransient<IOperationFailedExceptionPublisher, HxMessengerOperationFailedExceptionPublisher>();
			builder.Services.AddTransient<AuthorizationGrpcClientInterceptor>();
			builder.Services.AddGrpcClientInfrastructure(assemblyToScanForDataContracts: typeof(Dto).Assembly);
			builder.Services.AddGrpcClientsByApiContractAttributes(
				typeof(IDataSeedFacade).Assembly,
				configureGrpcClientWithAuthorization: grpcClient =>
				{
					grpcClient.AddHttpMessageHandler(provider =>
					{
						var navigationManager = provider.GetRequiredService<NavigationManager>();
						var backendUrl = navigationManager.BaseUri;

						return provider.GetRequiredService<AuthorizationMessageHandler>()
							.ConfigureHandler(authorizedUrls: new[] { backendUrl }); // TODO? as neede: , scopes: new[] { "havit-NewProjectTemplate-api" });
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
			var instrumentationKey = builder.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

			builder.Services.AddBlazorApplicationInsights(async applicationInsights =>
			{
				await applicationInsights.SetInstrumentationKey(instrumentationKey);
				await applicationInsights.LoadAppInsights();

				var telemetryItem = new TelemetryItem()
				{
					Tags = new Dictionary<string, object>()
					{
						{ "ai.cloud.role", "Web.Client" },
						// { "ai.cloud.roleInstance", "..." },
					}
				};

				await applicationInsights.AddTelemetryInitializer(telemetryItem);
			}, addILoggerProvider: true);

			builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(level => (level == LogLevel.Error) || (level == LogLevel.Critical));

#if DEBUG
			builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
		}
	}
}
