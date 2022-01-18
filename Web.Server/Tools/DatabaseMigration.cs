using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Web.Server.Tools;

public static class DatabaseMigration
{
	public static void UpgradeDatabaseSchemaAndData(this IApplicationBuilder app)
	{
		using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var context = serviceScope.ServiceProvider.GetService<IDbContext>();
			context.Database.Migrate();

			var dataSeedRunner = serviceScope.ServiceProvider.GetService<IDataSeedRunner>();
			dataSeedRunner.SeedData<CoreProfile>();
		}
	}
}
