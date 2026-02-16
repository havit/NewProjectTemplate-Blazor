using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

var builder = DistributedApplication.CreateBuilder(args);

// Read launch profile configuration from environment variables
var servicesMode = Environment.GetEnvironmentVariable("APPHOST_SERVICES") ?? "All";
var forceContainerDatabase = Environment.GetEnvironmentVariable("APPHOST_FORCE_CONTAINER_DATABASE") == "true";

var connectionString = builder.Configuration.GetConnectionString("Database");

var webServerBuilder = builder.AddProject<Projects.Web_Server>("web-server");

// Add JobsRunner only if servicesMode is "All"
IResourceBuilder<ProjectResource> jobsRunnerBuilder = null;
if (servicesMode == "All")
{
	jobsRunnerBuilder = builder.AddProject<Projects.JobsRunner>("jobsrunner")
		.WithExplicitStart();
}

// Determine database configuration
bool useContainerDatabase = forceContainerDatabase || (string.IsNullOrEmpty(connectionString) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

if (!string.IsNullOrEmpty(connectionString))
{
	// Connection string is defined in AppHost configuration - propagate it to services
	var databaseResource = builder.AddConnectionString("Database");

	webServerBuilder.WithReference(databaseResource);
	jobsRunnerBuilder?.WithReference(databaseResource);
}
else if (useContainerDatabase)
{
	// No connection string and (non-Windows platform OR forced) - use Aspire-managed SQL Server container
	var sqlServerResource = builder.AddSqlServer("SqlServer")
		.WithLifetime(ContainerLifetime.Persistent)
		.WithDataVolume();

	var databaseResource = sqlServerResource.AddDatabase("Database");

	webServerBuilder
		.WithReference(databaseResource)
		.WaitFor(databaseResource);

	jobsRunnerBuilder?
		.WithReference(databaseResource)
		.WaitFor(databaseResource);
}
// else: Windows without connection string - services use their own connection strings from appsettings (localdb)

builder.Build().Run();