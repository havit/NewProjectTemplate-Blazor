using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Havit.NewProjectTemplate.DependencyInjection.Configuration;

/// <summary>
/// Sets up Azure KeyVault as a configuration provider.
/// </summary>
public static class KeyVaultConfig
{
	public static IConfigurationBuilder AddCustomizedAzureKeyVault(this IConfigurationManager configurationManager)
	{
		string keyVaultUri = configurationManager.GetConnectionString("AzureKeyVault");

		if (!String.IsNullOrEmpty(keyVaultUri))
		{
			configurationManager.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
		}

		return configurationManager;
	}
}
