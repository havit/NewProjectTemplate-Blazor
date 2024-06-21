using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Services.Infrastructure.Logging;

public static class AzureWebAppDiagnosticsExtensions
{
	public static ILoggingBuilder AddCustomizedAzureWebAppDiagnostics(this ILoggingBuilder builder, string appName)
	{
		builder.AddAzureWebAppDiagnostics((AzureBlobLoggerOptions options) =>
		{
			string _appName = appName;

			options.BlobName = "log.txt";
			options.FileNameFormat = context =>
			{
				// context.AppName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")
				// context.Identifier = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID") + "_" + options.BlobName
				var timestamp = context.Timestamp;
				return $"{context.AppName}_{timestamp.Year}_{timestamp.Month:00}_{timestamp.Day:00}_{_appName}/{timestamp.Hour:00}_{context.Identifier}";
			};

			options.FlushPeriod = TimeSpan.FromSeconds(10);
		});

		// Are we running in Azure App Service?
		// Inspiration: https://github.com/dotnet/aspnetcore/blob/c00e0e775208cb7cb377f9bd0c8a66a0b3d0ed4d/src/Logging.AzureAppServices/src/WebAppContext.cs#L30
		if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")) && !string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("HOME")))
		{
			// See comment in ConfigurationBasedLevelSwitcherRemoval class
			builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>, ConfigurationBasedLevelSwitcherRemoval>();
		}
		return builder;
	}

}
