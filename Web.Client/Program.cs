using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorApplicationInsights;
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.NewProjectTemplate.Contracts.Crm;
using Havit.NewProjectTemplate.Contracts.System;
using Havit.NewProjectTemplate.Web.Client.Infrastructure;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Interceptors;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;
using Havit.NewProjectTemplate.Web.Client.Resources;
using Havit.NewProjectTemplate.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
			builder.Services.AddGeneratedResourceWrappers();

			builder.Services.AddBlazoredLocalStorage();

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
			// HxProgressOverlay.DefaultDelay = 0;
		}

		private static void AddGrpcClient(WebAssemblyHostBuilder builder)
		{
			builder.Services.AddGrpcClientInfrastructure();

			// TODO Mass registration of facades
			builder.Services.AddGrpcClientProxyWithAuth<IContactFacade>();

			builder.Services.AddGrpcClientProxyWithAuth<IDataSeedFacade>();
			builder.Services.AddGrpcClientProxyWithAuth<IMaintenanceFacade>();
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
			builder.Services.AddBlazorApplicationInsights(async applicationInsights =>
			{
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
