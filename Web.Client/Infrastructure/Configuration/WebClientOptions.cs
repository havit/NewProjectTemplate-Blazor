namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;

public record WebClientOptions
{
	/// <summary>
	/// Route to the client-side configuration.
	/// Defined as a constant, its usage shows "both ends" - how to use it in the client to download the configuration, and how to use it in the server to define the route for this configuration.
	/// </summary>
	public const string WebClientConfigurationRoute = "appsettings.WebClient.json";

	/// <summary>
	/// Path in appsettings.WebServer.json (not ...WebClient...), where the default configuration for the client is located.
	/// </summary>
	public const string Path = "WebClient";

	public ApplicationInsightsOptions ApplicationInsights { get; set; }
}
