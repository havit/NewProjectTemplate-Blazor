using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.LoggingExtensions;

/// <summary>
/// Upraví logování do File / Azure Blob Storage tak, aby ignorovalo nastavení úrovně logování v Azure portalu (Error, Warning, Information, Verbose).
/// Zůstane tak nastaveno výchozí chování logování .NET Core aplikací, tedy konfigurace dle konfigurace aplikace.
/// </summary>
/// <remarks>
/// Výchozí chování, které přepisujeme, je takové, že konfigurace logování v konfiguračním souboru je ignorována, a použije se nastavení úrovně logování
/// z Azure portálu. Toto nastavení je však použito pro veškeré namespaces a třídy, což je nepraktické.
/// Toto je v AddAzureWebAppDiagnostics zajištěno filtrem, který je registrován třídou ConfigurationBasedLevelSwitcher (https://github.com/dotnet/aspnetcore/blob/main/src/Logging.AzureAppServices/src/ConfigurationBasedLevelSwitcher.cs).
/// Zde kompenzujeme to, co dělá třída ConfigurationBasedLevelSwitcher. Ideální by bylo třídu ConfigurationBasedLevelSwitcher rovnou odebrat ze Services,
/// ale vzhledem k tomu, že je internal, bude se nám špatně poznávat.
/// </remarks>
internal class ConfigurationBasedLevelSwitcherRemoval : IConfigureOptions<LoggerFilterOptions>
{
	public void Configure(LoggerFilterOptions options)
	{
		// Služby jsou zaregistrovány jen při běhu v Azure App Service. Proto tato třída může být použita jen v Azure App Service.
		options.Rules.Remove(options.Rules.Single(rule => rule.ProviderName == typeof(Microsoft.Extensions.Logging.AzureAppServices.FileLoggerProvider).FullName));
		options.Rules.Remove(options.Rules.Single(rule => rule.ProviderName == typeof(Microsoft.Extensions.Logging.AzureAppServices.BlobLoggerProvider).FullName));
	}
}