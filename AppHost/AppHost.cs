using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);


// Check if connection string is defined in appsettings
var connectionString = builder.Configuration.GetConnectionString("Database");
var useAspireDatabase = string.IsNullOrEmpty(connectionString);

IResourceBuilder<IResourceWithConnectionString> databaseResource;

if (useAspireDatabase)
{
	// No connection string provided - use Aspire-managed SQL Server container
	var sqlServerResource = builder.AddSqlServer("cloudcore-sql", port: 1433)
		.WithLifetime(ContainerLifetime.Persistent)
		.WithDataVolume();

	databaseResource = sqlServerResource.AddDatabase("CisCloudCore");
}
else
{
	// Connection string is defined - use it
	databaseResource = builder.AddConnectionString("Database");
}

builder.AddProject<Projects.Web_Server>("web-server")
	.WithReference(databaseResource)
	.WaitFor(databaseResource);

builder.AddProject<Projects.JobsRunner>("jobsrunner")
	.WithReference(databaseResource)
	.WaitFor(databaseResource)
	.WithExplicitStart();

builder.Build().Run();