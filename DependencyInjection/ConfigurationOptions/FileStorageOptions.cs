namespace Havit.NewProjectTemplate.DependencyInjection.ConfigurationOptions;

public class FileStorageOptions
{
	public const string FileStorageOptionsKey = "AppSettings:FileStorage";

	public string PathOrContainerName { get; set; }
}
