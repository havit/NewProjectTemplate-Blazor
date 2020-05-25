using System;
using System.Collections.Generic;
using System.Linq;
using Havit.Data.Patterns.DataSeeds;
using Havit.Data.Patterns.DataSeeds.Profiles;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Seeds.Core;
using Havit.GoranG3.Facades.Infrastructure.Security;
using Havit.GoranG3.Facades.Infrastructure.Security.Authorization;
using Havit.GoranG3.Services.Infrastructure;

namespace Havit.GoranG3.Facades.System
{
	/// <summary>
	/// Fasáda k seedování dat.
	/// </summary>
	[Service]
	public class DataSeedFacade : IDataSeedFacade
	{
		private readonly IDataSeedRunner dataSeedRunner;
		private readonly IApplicationAuthorizationService applicationAuthorizationService;

		public DataSeedFacade(IDataSeedRunner dataSeedRunner, IApplicationAuthorizationService applicationAuthorizationService)
		{
			this.dataSeedRunner = dataSeedRunner;
			this.applicationAuthorizationService = applicationAuthorizationService;
		}

		/// <summary>
		/// Provede seedování dat daného profilu.
		/// Pokud jde produkční prostředí a profil není pro produkční prostředí povolen, vrací BadRequest.
		/// </summary>
		public void SeedDataProfile(string profileName)
		{
			applicationAuthorizationService.VerifyCurrentUserAuthorization(Operations.SystemAdministration);

			Type type = GetProfileTypes().FirstOrDefault(item => String.Equals(item.Name, profileName, StringComparison.InvariantCultureIgnoreCase));

			if (type == null)
			{
				throw new OperationFailedException($"Profil {profileName} nebyl nalezen.");
			}

			dataSeedRunner.SeedData(type);
		}

		/// <summary>
		/// Returns list of available data seed profiles (names are ready for use as parameter to <see cref="SeedDataProfile"/> method).
		/// </summary>
		public IList<string> GetDataSeedProfiles()
		{
			return GetProfileTypes()
				.Select(t => t.Name)
				.ToList();
		}

		private static IEnumerable<Type> GetProfileTypes()
		{
			return typeof(CoreProfile).Assembly.GetTypes()
				.Where(t => t.GetInterfaces().Contains(typeof(IDataSeedProfile)));
		}
	}
}
