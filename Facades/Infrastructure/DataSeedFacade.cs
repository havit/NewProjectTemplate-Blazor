using Havit.Data.Patterns.DataSeeds;
using Havit.Data.Patterns.DataSeeds.Profiles;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.DataLayer.Seeds.Core;
using Havit.NewProjectTemplate.Model.Security;
using Havit.Services.Caching;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure;

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
	/// Executes seed for the selected profile.
	/// </summary>
	public Task SeedDataProfileAsync(Dto<string> profileName, CancellationToken cancellationToken = default)
	{
		// applicationAuthorizationService.VerifyCurrentUserAuthorization(Operations.SystemAdministration); // TODO alternative authorization approach

		Type type = GetProfileTypes().FirstOrDefault(item => string.Equals(item.Name, profileName.Value, StringComparison.InvariantCultureIgnoreCase));

		if (type == null)
		{
			throw new OperationFailedException($"DataSeedProfile {profileName.Value} not found.");
		}

		// Individual seeds do not invalidate cache. If there are any cached entries (incl. empty-GetAll),
		// they get seeded and another seed asks for GetAll(), the newly seeded entities are not included.
		cacheService.Clear();

		dataSeedRunner.SeedData(type, forceRun: true);

		cacheService.Clear();

		return Task.CompletedTask;
	}

	/// <summary>
	/// Returns list of available data seed profiles (names are ready for use as parameter to <see cref="SeedDataProfileAsync"/> method).
	/// </summary>
	public Task<List<string>> GetDataSeedProfilesAsync(CancellationToken cancellationToken = default)
	{
		return Task.FromResult(GetProfileTypes()
						.Select(t => t.Name)
						.ToList()
		);
	}

	private static IEnumerable<Type> GetProfileTypes()
	{
		return typeof(CoreProfile).Assembly.GetTypes()
			.Where(t => t.GetInterfaces().Contains(typeof(IDataSeedProfile)));
	}
}
