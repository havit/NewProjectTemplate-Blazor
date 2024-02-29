namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public class AzureAdOptions
{
	public const string Path = "AzureAd";

	public string Instance { get; set; }
	public string TenantId { get; set; }
	public string ClientId { get; set; }
	public string ClientSecret { get; set; }

	public string GetAutorityUrl() => $"{Instance}/{TenantId}/v2.0";
}
