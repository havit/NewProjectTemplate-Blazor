using System;
using System.IO;
using System.Runtime.CompilerServices;
using Havit.GoranG3.DataLayer.Seeds.Core.Common;
using Havit.GoranG3.Entity;
using Havit.GoranG3.Services.Infrastructure;
using Havit.GoranG3.Services.Infrastructure.TimeService;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.Extensions.DependencyInjection;
using Havit.Services;
using Havit.Services.TimeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Havit.Services.Caching;
using System.Runtime.Caching;
using Microsoft.Extensions.DependencyInjection;
using Havit.Data.EntityFrameworkCore.Patterns.DependencyInjection;
using Havit.GoranG3.DataLayer.DataSources.Common;

namespace Havit.GoranG3.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static IServiceCollection ConfigureForWebServer(this IServiceCollection services, IConfiguration configuration)
		{
			InstallConfiguration installConfiguration = new InstallConfiguration
			{
				DatabaseConnectionString = configuration.GetConnectionString("Database"),
				ServiceProfiles = new[] { ServiceAttribute.DefaultProfile, ServiceProfiles.WebServer },
			};

			return services.ConfigureForAll(installConfiguration);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static IServiceCollection ConfigureForTests(this IServiceCollection services, bool useInMemoryDb = true)
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Developement";

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{environment}.json", true)
				.Build();

			InstallConfiguration installConfiguration = new InstallConfiguration
			{
				DatabaseConnectionString = configuration.GetConnectionString("Database"),
				ServiceProfiles = new[] { ServiceAttribute.DefaultProfile },
				UseInMemoryDb = useInMemoryDb,
			};

			return services.ConfigureForAll(installConfiguration);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IServiceCollection ConfigureForAll(this IServiceCollection services, InstallConfiguration installConfiguration)
		{
			InstallHavitEntityFramework(services, installConfiguration);
			InstallHavitServices(services);
			InstallByServiceAttribute(services, installConfiguration);
			InstallAuthorizationHandlers(services);

			services.AddMemoryCache(); // ie. IClaimsCacheStorage

			return services;
		}

		private static void InstallHavitEntityFramework(IServiceCollection services, InstallConfiguration configuration)
		{
			DbContextOptions options = configuration.UseInMemoryDb
				? new DbContextOptionsBuilder<GoranG3DbContext>().UseInMemoryDatabase(nameof(GoranG3DbContext)).Options
				: new DbContextOptionsBuilder<GoranG3DbContext>().UseSqlServer(configuration.DatabaseConnectionString, c => c.MaxBatchSize(30)).Options;

			services.WithEntityPatternsInstaller()
				.AddEntityPatterns()
				//.AddLocalizationServices<Language>()
				.AddDbContext<GoranG3DbContext>(options)
				.AddDataLayer(typeof(IApplicationSettingsDataSource).Assembly);
		}

		private static void InstallHavitServices(IServiceCollection services)
		{
			// HAVIT .NET Framework Extensions
			services.AddSingleton<ITimeService, ApplicationTimeService>();
			services.AddSingleton<ICacheService, MemoryCacheService>();
			services.AddSingleton(new MemoryCacheServiceOptions { UseCacheDependenciesSupport = false });
		}

		private static void InstallByServiceAttribute(IServiceCollection services, InstallConfiguration configuration)
		{

			services.AddByServiceAttribute(typeof(Havit.GoranG3.DataLayer.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
			services.AddByServiceAttribute(typeof(Havit.GoranG3.Services.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
			services.AddByServiceAttribute(typeof(Havit.GoranG3.Facades.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
		}

		private static void InstallAuthorizationHandlers(IServiceCollection services)
		{
			services.Scan(scan => scan.FromAssemblyOf<Havit.GoranG3.Services.Properties.AssemblyInfo>()
				.AddClasses(classes => classes.AssignableTo<IAuthorizationHandler>())
				.As<IAuthorizationHandler>()
				.WithScopedLifetime()
			);
		}
	}
}