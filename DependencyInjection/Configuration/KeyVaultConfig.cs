using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Havit.NewProjectTemplate.DependencyInjection.Configuration;

/// <summary>
/// Sets up Azure KeyVault as a configuration provider.
/// The location here in DependencyInjection is not ideal, but it is probably the best place that is shared by both Utility and Web.Server.
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
