using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

var builder = DistributedApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

var webServerBuilder = builder.AddProject<Projects.Web_Server>("web-server");
var jobsRunnerBuilder = builder.AddProject<Projects.JobsRunner>("jobsrunner")
	.WithExplicitStart();

if (!string.IsNullOrEmpty(connectionString))
{
	// Connection string is defined in AppHost configuration - propagate it to services
	var databaseResource = builder.AddConnectionString("Database");

	webServerBuilder.WithReference(databaseResource);
	jobsRunnerBuilder.WithReference(databaseResource);
}
else if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
	// No connection string and non-Windows platform - use Aspire-managed SQL Server container
	var sqlServerResource = builder.AddSqlServer("SqlServer")
		.WithLifetime(ContainerLifetime.Persistent)
		.WithDataVolume();

	var databaseResource = sqlServerResource.AddDatabase("Database");

	webServerBuilder
		.WithReference(databaseResource)
		.WaitFor(databaseResource);

	jobsRunnerBuilder
		.WithReference(databaseResource)
		.WaitFor(databaseResource);
}
// else: Windows without connection string - services use their own connection strings from appsettings (localdb)

builder.Build().Run();