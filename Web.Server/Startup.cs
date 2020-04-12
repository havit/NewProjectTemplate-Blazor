using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Havit.GoranG3.Facades.Infrastructure.Security.Identity;
using Havit.GoranG3.Model.Security;
using Havit.GoranG3.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Havit.GoranG3.Web.Server.Tools;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Havit.GoranG3.Web.Server.Infrastructure.Security;
using IdentityServer4.Models;

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

			services.AddOptions(); // Adds services required for using options.

			// TODO services.AddCustomizedMailing(configuration);

			// TODO services.AddExceptionMonitoring(configuration);

			// TODO services.AddApplicationInsightsTelemetry(configuration);

			services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddUserStore<UserStore>();

			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddClients()
				.AddSigningCredentials()
				.AddInMemoryIdentityResources(new IdentityResource[]
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Profile()
				})
				.AddInMemoryApiResources(new[] { new ApiResource()
				{
					// TODO IdentityServer ApiResources
				}});

			services.AddAuthentication()
				.AddIdentityServerJwt();

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();
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

			// TODO app.UseExceptionMonitoring();

			app.UseRouting();

			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});

			app.UpgradeDatabaseSchemaAndData();
		}
	}
}
