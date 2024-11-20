﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Havit.Data.EntityFrameworkCore.Patterns.DependencyInjection;
using Havit.Data.EntityFrameworkCore.Patterns.Infrastructure;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataSources;
using Havit.Data.Patterns.Infrastructure;
using Havit.Data.Patterns.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Havit.NewProjectTemplate.DataLayer;

[System.CodeDom.Compiler.GeneratedCode("Havit.Data.EntityFrameworkCore.CodeGenerator", "1.0")]
public static partial class DataLayerServiceExtensions
{
	public static IServiceCollection AddDataLayerServices(this IServiceCollection services, ComponentRegistrationOptions options = null)
	{
		services.AddDataLayerCoreServices(options);
	
		AddDataSources(services);
		AddRepositories(services);
		AddDataEntries(services);
		AddEntityKeyAccessors(services);

		AddNextDataLayerServices(services); // partial method for custom extensibility

		return services;
	}

	private static void AddDataSources(IServiceCollection services)
	{
		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Common.IApplicationSettingsDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Common.ApplicationSettingsDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>, Havit.NewProjectTemplate.DataLayer.DataSources.Common.ApplicationSettingsDbDataSource>();

		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Common.ICountryDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Common.CountryDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Common.Country>, Havit.NewProjectTemplate.DataLayer.DataSources.Common.CountryDbDataSource>();

		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Common.ICountryLocalizationDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Common.CountryLocalizationDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Common.CountryLocalization>, Havit.NewProjectTemplate.DataLayer.DataSources.Common.CountryLocalizationDbDataSource>();

		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Localizations.ILanguageDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Localizations.LanguageDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Localizations.Language>, Havit.NewProjectTemplate.DataLayer.DataSources.Localizations.LanguageDbDataSource>();

		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Security.IRoleDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Security.RoleDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Security.Role>, Havit.NewProjectTemplate.DataLayer.DataSources.Security.RoleDbDataSource>();

		services.TryAddTransient<Havit.NewProjectTemplate.DataLayer.DataSources.Security.IUserDataSource, Havit.NewProjectTemplate.DataLayer.DataSources.Security.UserDbDataSource>();
		services.TryAddTransient<IDataSource<Havit.NewProjectTemplate.Model.Security.User>, Havit.NewProjectTemplate.DataLayer.DataSources.Security.UserDbDataSource>();

	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Common.IApplicationSettingsRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Common.ApplicationSettingsDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Common.IApplicationSettingsRepository>());

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Common.ICountryRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Common.CountryDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Common.Country>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Common.ICountryRepository>());

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Common.ICountryLocalizationRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Common.CountryLocalizationDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Common.CountryLocalization>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Common.ICountryLocalizationRepository>());

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Localizations.ILanguageRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Localizations.LanguageDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Localizations.Language>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Localizations.ILanguageRepository>());

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Security.IRoleRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Security.RoleDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Security.Role>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Security.IRoleRepository>());

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.Repositories.Security.IUserRepository, Havit.NewProjectTemplate.DataLayer.Repositories.Security.UserDbRepository>();
		services.TryAddScoped<IRepository<Havit.NewProjectTemplate.Model.Security.User>>(sp => sp.GetRequiredService<Havit.NewProjectTemplate.DataLayer.Repositories.Security.IUserRepository>());

	}

	private static void AddDataEntries(IServiceCollection services)
	{
		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.DataEntries.Common.IApplicationSettingsEntries, Havit.NewProjectTemplate.DataLayer.DataEntries.Common.ApplicationSettingsEntries>();

		services.TryAddScoped<Havit.NewProjectTemplate.DataLayer.DataEntries.Localizations.ILanguageEntries, Havit.NewProjectTemplate.DataLayer.DataEntries.Localizations.LanguageEntries>();

	}

	private static void AddEntityKeyAccessors(IServiceCollection services)
	{
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.ApplicationSettings, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.ApplicationSettings, int>>();
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.Country, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.Country, int>>();
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.CountryLocalization, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Common.CountryLocalization, int>>();
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Localizations.Language, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Localizations.Language, int>>();
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Security.Role, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Security.Role, int>>();
		services.TryAddTransient<IEntityKeyAccessor<Havit.NewProjectTemplate.Model.Security.User, int>, DbEntityKeyAccessor<Havit.NewProjectTemplate.Model.Security.User, int>>();
	}

	static partial void AddNextDataLayerServices(IServiceCollection services);
}
