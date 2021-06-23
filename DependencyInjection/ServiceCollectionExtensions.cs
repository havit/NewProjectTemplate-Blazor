using System;
using System.IO;
using System.Runtime.CompilerServices;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core.Common;
using Havit.NewProjectTemplate.Entity;
using Havit.NewProjectTemplate.Services.Infrastructure;
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
using Havit.NewProjectTemplate.DataLayer.DataSources.Common;
using Havit.Data.EntityFrameworkCore.Patterns.UnitOfWorks.EntityValidation;
using Havit.NewProjectTemplate.Services.TimeServices;
using Havit.NewProjectTemplate.DataLayer.Repositories.Crm;
using Havit.NewProjectTemplate.DependencyInjection.ConfigrationOptions;
using Havit.Services.FileStorage;
using Microsoft.AspNetCore.StaticFiles;
using Havit.NewProjectTemplate.Services.Infrastructure.FileStorages;
using Azure.Identity;
using Havit.Services.Azure.FileStorage;

namespace Havit.NewProjectTemplate.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static IServiceCollection ConfigureForWebServer(this IServiceCollection services, IConfiguration configuration)
		{
			FileStorageOptions fileStorageOptions = new FileStorageOptions();
			configuration.GetSection(FileStorageOptions.FileStorageOptionsKey).Bind(fileStorageOptions);

			InstallConfiguration installConfiguration = new InstallConfiguration
			{
				DatabaseConnectionString = configuration.GetConnectionString("Database"),
				AzureStorageConnectionString = configuration.GetConnectionString("AzureStorageConnectionString"),
				FileStoragePathOrContainerName = fileStorageOptions.PathOrContainerName,
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
			InstallFileServices(services, installConfiguration);

			services.AddMemoryCache();

			return services;
		}

		private static void InstallHavitEntityFramework(IServiceCollection services, InstallConfiguration configuration)
		{
			DbContextOptions options = configuration.UseInMemoryDb
				? new DbContextOptionsBuilder<NewProjectTemplateDbContext>().UseInMemoryDatabase(nameof(NewProjectTemplateDbContext)).Options
				: new DbContextOptionsBuilder<NewProjectTemplateDbContext>().UseSqlServer(configuration.DatabaseConnectionString, c => c.MaxBatchSize(30)).Options;

			services.WithEntityPatternsInstaller()
				.AddEntityPatterns()
				//.AddLocalizationServices<Language>()
				.AddDbContext<NewProjectTemplateDbContext>(options)
				.AddDataLayer(typeof(IApplicationSettingsDataSource).Assembly)
				.AddLookupService<ICountryByIsoCodeLookupService, CountryByIsoCodeLookupService>();

			services.AddSingleton<IEntityValidator<object>, ValidatableObjectEntityValidator>();
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

			services.AddByServiceAttribute(typeof(Havit.NewProjectTemplate.DataLayer.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
			services.AddByServiceAttribute(typeof(Havit.NewProjectTemplate.Services.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
			services.AddByServiceAttribute(typeof(Havit.NewProjectTemplate.Facades.Properties.AssemblyInfo).Assembly, configuration.ServiceProfiles);
		}

		private static void InstallAuthorizationHandlers(IServiceCollection services)
		{
			services.Scan(scan => scan.FromAssemblyOf<Havit.NewProjectTemplate.Services.Properties.AssemblyInfo>()
				.AddClasses(classes => classes.AssignableTo<IAuthorizationHandler>())
				.As<IAuthorizationHandler>()
				.WithScopedLifetime()
			);
		}

		private static void InstallFileServices(IServiceCollection services, InstallConfiguration configuration)
		{
			services.AddFileStorageWrappingService<IApplicationFileStorageService, ApplicationFileStorageService, ApplicationFileStorage>();

			if (!String.IsNullOrWhiteSpace(configuration.AzureStorageConnectionString))
			{
				services.AddAzureBlobStorageService<ApplicationFileStorage>(new AzureBlobStorageServiceOptions<ApplicationFileStorage>
				{
					BlobStorage = configuration.AzureStorageConnectionString,
					ContainerName = configuration.FileStoragePathOrContainerName,
					TokenCredential = new DefaultAzureCredential()
				});
			}
			else
			{
				services.AddFileSystemStorageService<ApplicationFileStorage>(configuration.FileStoragePathOrContainerName?.Replace("%TEMP%", Path.GetTempPath()));
			}
		}
	}
}