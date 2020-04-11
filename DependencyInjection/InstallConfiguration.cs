using System;

namespace Havit.GoranG3.DependencyInjection
{
	internal class InstallConfiguration
	{
		public string DatabaseConnectionString { get; set; }
		public string[] ServiceProfiles { get; set; }
		public bool UseInMemoryDb { get; internal set; }
	}
}