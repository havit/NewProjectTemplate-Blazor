using System;
using Havit.Extensions.DependencyInjection;
using Havit.GoranG3.DependencyInjection;
using Havit.GoranG3.G2Migrator;
using Havit.GoranG3.G2Migrator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace G2Migrator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var serviceProvider = new ServiceCollection()
				.ConfigureForG2Migrator(out var configuration)
				.Configure<MigrationOptions>(configuration.GetSection("Migration"))
				.AddByServiceAttribute(typeof(Program).Assembly)
				.BuildServiceProvider();

			var migrationService = serviceProvider.GetService<IG2MigrationRunner>();
			migrationService.Run();
		}
	}
}
