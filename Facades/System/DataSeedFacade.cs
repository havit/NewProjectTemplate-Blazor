using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.Data.Patterns.DataSeeds.Profiles;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.System;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Services.Caching;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.System
{
	/// <summary>
	/// Fasáda k seedování dat.
	/// </summary>
	[Service]
	[Authorize(Roles = nameof(Role.Entry.SystemAdministrator))]

	public class DataSeedFacade : IDataSeedFacade
	{
		private readonly IDataSeedRunner dataSeedRunner;
		private readonly ICacheService cacheService;

		public DataSeedFacade(
			IDataSeedRunner dataSeedRunner,
			ICacheService cacheService)
		{
			this.dataSeedRunner = dataSeedRunner;
			this.cacheService = cacheService;
		}

		/// <summary>
		/// Provede seedování dat daného profilu.
		/// Pokud jde produkční prostředí a profil není pro produkční prostředí povolen, vrací BadRequest.
		/// </summary>
		public Task SeedDataProfile(string profileName)
		{
			// applicationAuthorizationService.VerifyCurrentUserAuthorization(Operations.SystemAdministration); // TODO alternative authorization approach

			Type type = GetProfileTypes().FirstOrDefault(item => String.Equals(item.Name, profileName, StringComparison.InvariantCultureIgnoreCase));

			if (type == null)
			{
				throw new OperationFailedException($"Profil {profileName} nebyl nalezen.");
			}

			dataSeedRunner.SeedData(type, forceRun: true);

			cacheService.Clear();

			return Task.CompletedTask;
		}

		/// <summary>
		/// Returns list of available data seed profiles (names are ready for use as parameter to <see cref="SeedDataProfile"/> method).
		/// </summary>
		public Task<Dto<string[]>> GetDataSeedProfiles()
		{
			return Task.FromResult(Dto.FromValue(GetProfileTypes()
							.Select(t => t.Name)
							.ToArray()
			));
		}

		private static IEnumerable<Type> GetProfileTypes()
		{
			return typeof(CoreProfile).Assembly.GetTypes()
				.Where(t => t.GetInterfaces().Contains(typeof(IDataSeedProfile)));
		}
	}
}
