using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.LoggingExtensions;

public static class AzureWebAppDiagnosticsExtensions
{
	public static ILoggingBuilder AddCustomizedAzureWebAppDiagnostics(this ILoggingBuilder builder)
	{
		builder.AddAzureWebAppDiagnostics();

		// bežíme v Azure App Service?
		// inspirace: https://github.com/dotnet/aspnetcore/blob/c00e0e775208cb7cb377f9bd0c8a66a0b3d0ed4d/src/Logging.AzureAppServices/src/WebAppContext.cs#L30
		if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")) && !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("HOME")))
		{
			// viz komentář u třídy ConfigurationBasedLevelSwitcherRemoval
			builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>, ConfigurationBasedLevelSwitcherRemoval>();
		}
		return builder;
	}

}