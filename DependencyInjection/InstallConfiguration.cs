namespace Havit.NewProjectTemplate.DependencyInjection;

internal class InstallConfiguration
{
	public string DatabaseConnectionString { get; set; }
	public string[] ServiceProfiles { get; set; }
	public bool UseInMemoryDb { get; internal set; }

	public string ApiCommunicationLogStorage { get; set; }
	public string AzureStorageConnectionString { get; set; }
	public string FileStoragePathOrContainerName { get; set; }
}
