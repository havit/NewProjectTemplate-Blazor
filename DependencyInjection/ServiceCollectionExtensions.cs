using System.Runtime.CompilerServices;
using Azure.Identity;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.DependencyInjection;
using Havit.Data.EntityFrameworkCore.Patterns.UnitOfWorks.EntityValidation;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer;
using Havit.NewProjectTemplate.DataLayer.Repositories.Common;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Havit.NewProjectTemplate.DependencyInjection.ConfigurationOptions;
using Havit.NewProjectTemplate.Entity;
using Havit.NewProjectTemplate.Facades;
using Havit.NewProjectTemplate.Model.Localizations;
using Havit.NewProjectTemplate.Services;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.NewProjectTemplate.Services.Infrastructure.FileStorages;
using Havit.NewProjectTemplate.Services.Infrastructure.MigrationTool;
using Havit.NewProjectTemplate.Services.TimeServices;
using Havit.Services.Azure.FileStorage;
using Havit.Services.Caching;
using Havit.Services.FileStorage;
using Havit.Services.TimeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.DependencyInjection;

public static class ServiceCollectionExtensions
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static IServiceCollection ConfigureForWebServer(this IServiceCollection services, IConfiguration configuration)
	{
		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			Configuration = configuration,
			DatabaseConnectionString = configuration.GetConnectionString("Database"),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile, ServiceProfiles.WebServer },
			UseInMemoryDb = false
		};

		return services.ConfigureForAll(installConfiguration);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static IServiceCollection ConfigureForJobsRunner(this IServiceCollection services, IConfiguration configuration)
	{
		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			Configuration = configuration,
			DatabaseConnectionString = configuration.GetConnectionString("Database"),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile, ServiceProfiles.JobsRunner },
			UseInMemoryDb = false
		};

		return services.ConfigureForAll(installConfiguration);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static IServiceCollection ConfigureForTests(this IServiceCollection services, bool useInMemoryDb = true)
	{
		string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appSettings.json")
			.AddJsonFile($"appSettings.{environment}.json", true)
			.AddJsonFile($"appSettings.{environment}.local.json", true) // .gitignored
			.Build();

		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			Configuration = configuration,
			DatabaseConnectionString = AddAgentNameToDatabaseName(configuration.GetConnectionString("Database")),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile },
			UseInMemoryDb = useInMemoryDb
		};

		services.AddSingleton<IConfiguration>(configuration);
		return services.ConfigureForAll(installConfiguration);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static IServiceCollection ConfigureForMigrationTool(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDb = true)
	{
		InstallConfiguration installConfiguration = new InstallConfiguration
		{
			Configuration = configuration,
			DatabaseConnectionString = configuration.GetConnectionString("Database"),
			ServiceProfiles = new[] { ServiceAttribute.DefaultProfile },
			UseInMemoryDb = false
		};

		InstallHavitEntityFramework(services, installConfiguration);
		InstallHavitServices(services);

		services.AddDataLayerByServiceAttribute(installConfiguration.ServiceProfiles);
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

		return services;
	}

	private static void InstallHavitEntityFramework(IServiceCollection services, InstallConfiguration configuration)
	{
		services.AddDbContext<IDbContext, NewProjectTemplateDbContext>(optionsBuilder =>
			{
				if (configuration.UseInMemoryDb)
				{
					optionsBuilder.UseInMemoryDatabase(nameof(NewProjectTemplateDbContext));
				}
				else
				{
					optionsBuilder.UseSqlServer(configuration.DatabaseConnectionString, c => c.MaxBatchSize(30));
				}
				optionsBuilder.UseDefaultHavitConventions();
			})
			.AddDataLayerServices()
			.AddDataSeeds(typeof(CoreProfile).Assembly)
			.AddLocalizationServices<Language>()
			.AddLookupService<ICountryByIsoCodeLookupService, CountryByIsoCodeLookupService>();

		services.AddSingleton<IEntityValidator<object>, ValidatableObjectEntityValidator>();
	}

	private static void InstallHavitServices(IServiceCollection services)
	{
		// HAVIT .NET Framework Extensions
		services.AddSingleton<ITimeService, ApplicationTimeService>();
		services.AddMemoryCache();
		services.AddSingleton<ICacheService, MemoryCacheService>();
		services.AddSingleton(new MemoryCacheServiceOptions { UseCacheDependenciesSupport = false });
	}

	private static void InstallByServiceAttribute(IServiceCollection services, InstallConfiguration configuration)
	{
		services.AddDataLayerByServiceAttribute(configuration.ServiceProfiles);
		services.AddServicesByServiceAttribute(configuration.ServiceProfiles);
		services.AddFacadesByServiceAttribute(configuration.ServiceProfiles);
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
		string azureStorageConnectionString = installConfiguration.Configuration.GetConnectionString("AzureStorage");
		FileStorageOptions fileStorageOptions = installConfiguration.Configuration.GetSection(FileStorageOptions.ApplicationFileStorageOptionsKey).Get<FileStorageOptions>();

		InstallFileStorageService<IApplicationFileStorageService, ApplicationFileStorageService, ApplicationFileStorage>(services, azureStorageConnectionString, fileStorageOptions?.PathOrContainerName);
	}

	internal static void InstallFileStorageService<TFileStorageService, TFileStorageImplementation, TFileStorageContext>(IServiceCollection services, string azureStorageConnectionString, string storagePath)
		where TFileStorageService : class, IFileStorageService<TFileStorageContext> // class covers all reference types here, e.g. IDocumentStorageService
		where TFileStorageImplementation : FileStorageWrappingService<TFileStorageContext>, TFileStorageService // e.g. DocumentStorageService
		where TFileStorageContext : FileStorageContext // e.g. DocumentStorage
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

	private static string AddAgentNameToDatabaseName(string databaseConnectionString)
	{
		string agentName = Environment.GetEnvironmentVariable("AGENT_NAME");
		if (!String.IsNullOrEmpty(agentName))
		{
			// pokud by connection string obsahoval heslo, musí být též Persist Security Info=true
			var databaseConnectionStringBuilder = new SqlConnectionStringBuilder(databaseConnectionString);
			databaseConnectionStringBuilder.InitialCatalog = $"{databaseConnectionStringBuilder.InitialCatalog}-{agentName}";
			return databaseConnectionStringBuilder.ToString();
		}
		else
		{
			return databaseConnectionString;
		}
	}
}
