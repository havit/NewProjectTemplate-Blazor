namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;

public record WebClientOptions
{
	/// <summary>
	/// Route ke konfiguraci klientské strany.
	/// Definováno jako konstanta, její použití ukazuje "oba konce" - jak použití v klientovi pro stažení konfigurace, tak použití v serveru pro definování routy pro tuto konfiguraci.
	/// </summary>
	public const string WebClientConfigurationRoute = "appsettings.WebClient.json";

	/// <summary>
	/// Cesta v appsettings.WebServer.json (nikoliv ...WebClient...), ve které je výchozí konfigurace pro klienta.
	/// </summary>
	public const string Path = "WebClient";

	public ApplicationInsightsOptions ApplicationInsights { get; set; }
}
