using System.IdentityModel.Tokens.Jwt;
using Havit.GoranG3.Contracts;
using Havit.GoranG3.DependencyInjection;
using Havit.GoranG3.Facades.Crm;
using Havit.GoranG3.Facades.Finance;
using Havit.GoranG3.Facades.Finance.Invoices;
using Havit.GoranG3.Facades.GrpcTests;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Havit.GoranG3.Facades.Infrastructure.Security.Identity;
using Havit.GoranG3.Facades.System;
using Havit.GoranG3.Model.Security;
using Havit.GoranG3.Web.Server.Infrastructure.ApplicationInsights;
using Havit.GoranG3.Web.Server.Infrastructure.Interceptors;
using Havit.GoranG3.Web.Server.Infrastructure.Security;
using Havit.GoranG3.Web.Server.Tools;
using IdentityServer4.Models;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;

namespace Havit.GoranG3.Web.Server
{
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

			services.AddOptions();

			// TODO services.AddCustomizedMailing(configuration);

			services.AddExceptionMonitoring(configuration);

			services.AddApplicationInsightsTelemetry(configuration);
			services.AddSingleton<ITelemetryInitializer, GrpcRequestStatusTelemetryInitializer>();
			services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });

			services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<Role>()
				.AddUserStore<UserStore>()
				.AddRoleStore<RoleStore>();

			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddClients()
				.AddSigningCredentials()
				.AddIdentityResources()
				.AddApiResources()
				.AddProfileService<IdentityServerProfileService>();

			services.AddAuthentication()
				.AddIdentityServerJwt();

			// server-side support for User.IsInRole(), see https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();

			services.AddSingleton<ServerExceptionsGrpcServerInterceptor>();

			services.AddSingleton(BinderConfiguration.Create());
			services.AddCodeFirstGrpc(config =>
			{
				config.Interceptors.Add<ServerExceptionsGrpcServerInterceptor>();
				config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
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
				endpoints.MapFallbackToFile("index.html");

				// TODO Mass-registration
				endpoints.MapGrpcService<TestFacade>();
				endpoints.MapGrpcService<InvoiceFacade>();

				endpoints.MapGrpcService<BankAccountFacade>();
				endpoints.MapGrpcService<ContactFacade>();
				endpoints.MapGrpcService<CurrencyFacade>();
				endpoints.MapGrpcService<ExchangeRateFacade>();

				endpoints.MapGrpcService<DataSeedFacade>();
				endpoints.MapGrpcService<MaintenanceFacade>();
			});

			app.UpgradeDatabaseSchemaAndData();
		}
	}
}
