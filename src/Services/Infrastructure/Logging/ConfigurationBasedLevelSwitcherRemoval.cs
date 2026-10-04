using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Services.Infrastructure.Logging;

/// <summary>
/// Modifies logging to File / Azure Blob Storage to ignore the logging level settings in the Azure portal (Error, Warning, Information, Verbose).
/// This preserves the default logging behavior of .NET Core applications, which is to use the application's configuration.
/// </summary>
/// <remarks>
/// The default behavior that we are overriding is that the logging configuration in the configuration file is ignored, and the logging level settings
/// from the Azure portal are used. However, this setting is applied to all namespaces and classes, which is impractical.
/// This is achieved in AddAzureWebAppDiagnostics by a filter registered by the ConfigurationBasedLevelSwitcher class (https://github.com/dotnet/aspnetcore/blob/main/src/Logging.AzureAppServices/src/ConfigurationBasedLevelSwitcher.cs).
/// Here, we compensate for what the ConfigurationBasedLevelSwitcher class does. Ideally, we would remove the ConfigurationBasedLevelSwitcher class from the Services altogether,
/// but since it is internal, it would be difficult to identify.
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