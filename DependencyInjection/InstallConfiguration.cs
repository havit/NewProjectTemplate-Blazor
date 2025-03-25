using Microsoft.Extensions.Configuration;

namespace Havit.NewProjectTemplate.DependencyInjection;

internal class InstallConfiguration
{
	public required IConfiguration Configuration { get; init; }
	public required string DatabaseConnectionString { get; set; }
	public required string[] ServiceProfiles { get; set; }
	public required bool UseInMemoryDb { get; internal set; }
}
