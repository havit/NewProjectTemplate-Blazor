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
using ProtoBuf.Grpc.Server;
using Havit.GoranG3.Facades.GrpcTests;
using Microsoft.AspNetCore.Identity;
using Web.Server.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;

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

			services.AddCodeFirstGrpc(config =>
			{
				config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
			});
			services.AddGrpcWeb(options =>
			{
				options.GrpcWebEnabled = true;
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

			// TODO app.UseExceptionMonitoring();

			app.UseRouting();

			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();

			app.UseGrpcWeb();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");

				// GRPC TESTs
				endpoints.MapGrpcService<TestFacade>();
			});

			app.UpgradeDatabaseSchemaAndData();
		}
	}
}
