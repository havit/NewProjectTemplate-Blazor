namespace Havit.NewProjectTemplate.DependencyInjection.ConfigurationOptions;

public class DataProtectionStorageOptions
{
	public const string DataProtectionStorageOptionsKey = "AppSettings:DataProtectionStorage";

	public string PathOrContainerName { get; set; }
}
