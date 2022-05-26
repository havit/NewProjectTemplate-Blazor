using System.IO;
using System.Runtime.CompilerServices;
using Azure.Identity;
using Havit.Data.EntityFrameworkCore.Patterns.DependencyInjection;
using Havit.Data.EntityFrameworkCore.Patterns.UnitOfWorks.EntityValidation;
using Havit.Extensions.DependencyInjection;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.DataSources.Common;
using Havit.NewProjectTemplate.DataLayer.Repositories.Common;
using Havit.NewProjectTemplate.DependencyInjection.ConfigrationOptions;
using Havit.NewProjectTemplate.Entity;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.NewProjectTemplate.Services.Infrastructure.FileStorages;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Havit.NewProjectTemplate.Services.Jobs;
using Havit.NewProjectTemplate.Services.TimeServices;
using Havit.Services.Azure.FileStorage;
using Havit.Services.Caching;
using Havit.Services.FileStorage;
using Havit.Services.TimeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.DependencyInjection;

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
	public static IServiceCollection ConfigureForUtility(this IServiceCollection services, IConfiguration configuration)
	{
		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			DatabaseConnectionString = configuration.GetConnectionString("Database"),
			AzureStorageConnectionString = configuration.GetConnectionString("AzureStorageConnectionString"),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile, ServiceProfiles.Utility },
			ApiCommunicationLogStorage = configuration.GetValue<string>("AppSettings:ApiCommunicationLogStorage:Path")
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
	public static IServiceCollection ConfigureForMigrationTool(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDb = true)
	{
		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			DatabaseConnectionString = configuration.GetConnectionString("Database"),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile },
		};

		InstallHavitEntityFramework(services, installConfiguration);
		InstallHavitServices(services);

		services.AddByServiceAttribute(typeof(DataLayer.Properties.AssemblyInfo).Assembly, installConfiguration.ServiceProfiles);
		services.AddSingleton<IMigrationService, MigrationService>();

		return services;
		// there is no ConfigureForAll (migration tool needs only DataLayer and DatabaseMigrationService)
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
		services.WithEntityPatternsInstaller()
			.AddEntityPatterns()
			//.AddLocalizationServices<Language>()
			.AddDbContext<NewProjectTemplateDbContext>(optionsBuilder =>
			{
				if (configuration.UseInMemoryDb)
				{
					optionsBuilder.UseInMemoryDatabase(nameof(NewProjectTemplateDbContext));
				}
				else
				{
					optionsBuilder.UseSqlServer(configuration.DatabaseConnectionString, c => c.MaxBatchSize(30));
				}
			})
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

	private static void InstallFileServices(IServiceCollection services, InstallConfiguration installConfiguration)
	{
		InstallFileStorageService<IApplicationFileStorageService, ApplicationFileStorageService, ApplicationFileStorage>(services, installConfiguration.AzureStorageConnectionString, installConfiguration.FileStoragePathOrContainerName);
	}

	internal static void InstallFileStorageService<TFileStorageService, TFileStorageImplementation, TFileStorageContext>(IServiceCollection services, string azureStorageConnectionString, string storagePath)
		where TFileStorageService : class, IFileStorageService<TFileStorageContext> // class zde znamená i interface! // např. IDocumentStorageService
		where TFileStorageImplementation : FileStorageWrappingService<TFileStorageContext>, TFileStorageService // např. DocumentStorageService
		where TFileStorageContext : FileStorageContext // např. DocumentStorage
	{
		services.AddFileStorageWrappingService<TFileStorageService, TFileStorageImplementation, TFileStorageContext>();

		if (!String.IsNullOrEmpty(azureStorageConnectionString))
		{
			services.AddAzureBlobStorageService<TFileStorageContext>(new AzureBlobStorageServiceOptions<TFileStorageContext>
			{
				BlobStorage = azureStorageConnectionString,
				ContainerName = storagePath,
				TokenCredential = new DefaultAzureCredential()
			});
		}
		else
		{
			services.AddFileSystemStorageService<TFileStorageContext>(storagePath);
		}
	}
}
