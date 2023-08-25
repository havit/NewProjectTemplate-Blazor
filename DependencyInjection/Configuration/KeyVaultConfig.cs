using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Havit.NewProjectTemplate.DependencyInjection.Configuration;

/// <summary>
/// Nastavuje Azure KeyVault jako configuration provider.
/// Umístění zde v DependencyInjection není šťastné, ale jde asi o nejlepší místo, které sdílí jak Utility, tak Web.Server.
/// </summary>
public static class KeyVaultConfig
{
	public static IConfigurationBuilder AddCustomizedAzureKeyVault(this IConfigurationBuilder builder)
	{
		string keyVaultUri = builder.Build().GetConnectionString("AzureKeyVault");

		if (!String.IsNullOrEmpty(keyVaultUri))
		{
			builder = builder.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
		}

		return builder;
	}
}