using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Havit.GoranG3.Web.Client.Infrastructure.Security;
using Havit.GoranG3.Contracts.Finance.Invoices;
using Havit.GoranG3.Web.Client.Infrastructure;

namespace Havit.GoranG3.Web.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory)); // multiple roles workaround
			builder.Services.AddApiAuthorization();
			builder.Services.AddLocalization();
			builder.Services.AddGrpcWebProxy<IInvoiceFacade>();

			await builder.Build().RunAsync();
		}
	}
}
