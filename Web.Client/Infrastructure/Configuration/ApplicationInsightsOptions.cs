namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;

public class ApplicationInsightsOptions
{
	/// <summary>
	/// Lokalita konfigurace ve appSettings.WebClient.json.
	/// </summary>
	public const string Path = "ApplicationInsights";

	public string ConnectionString { get; set; }
}