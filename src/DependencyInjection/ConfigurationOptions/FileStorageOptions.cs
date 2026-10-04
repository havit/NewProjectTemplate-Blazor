namespace Havit.NewProjectTemplate.DependencyInjection.ConfigurationOptions;

public class FileStorageOptions
{
	public const string ApplicationFileStorageOptionsKey = "AppSettings:ApplicationFileStorage";

	public string PathOrContainerName { get; set; }
}
