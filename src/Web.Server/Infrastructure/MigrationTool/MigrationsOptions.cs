namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.MigrationTool;

public class MigrationsOptions
{
	public const string Path = "AppSettings:Migrations";

	public bool RunMigrations { get; set; } = false;
}
