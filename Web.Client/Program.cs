using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Contracts.Finance;
using Havit.GoranG3.Contracts.Finance.Invoices;
using Havit.GoranG3.Contracts.GrpcTests;
using Havit.GoranG3.Contracts.System;
using Havit.GoranG3.Web.Client.Infrastructure;
using Havit.GoranG3.Web.Client.Infrastructure.Security;
using Havit.GoranG3.Web.Client.Resources;
using Havit.GoranG3.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Havit.GoranG3.Web.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
#if DEBUG
			builder.Logging.SetMinimumLevel(LogLevel.Trace);
#endif

			builder.RootComponents.Add<App>("app");

			builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory)); // multiple roles workaround
			builder.Services.AddApiAuthorization();

			builder.Services.AddLocalization();
			builder.Services.AddGeneratedResourceWrappers();

			builder.Services.AddBlazoredLocalStorage();

			builder.Services.AddHxMessenger();
			builder.Services.AddHxMessageBoxHost();

			builder.Services.AddScoped<ICurrencyDataStore, CurrencyDataStore>();

			AddGrpcClient(builder);

			WebAssemblyHost webAssemblyHost = builder.Build();

			await SetLanguage(webAssemblyHost);

			await webAssemblyHost.RunAsync();
		}

		private static void AddGrpcClient(WebAssemblyHostBuilder builder)
		{
			builder.Services.AddGrpcClientsInfrastructure();

			// TODO Mass registration of facades
			builder.Services.AddGrpcClientProxy<IInvoiceFacade>();
			builder.Services.AddGrpcClientProxy<ITestFacade>();

			builder.Services.AddGrpcClientProxy<IBankAccountFacade>();
			builder.Services.AddGrpcClientProxy<ICurrencyFacade>();

			builder.Services.AddGrpcClientProxy<IDataSeedFacade>();
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
	}
}
