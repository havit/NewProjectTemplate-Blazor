using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Model.Security;
using Havit.Services.Caching;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure;

[Service]
[Authorize(Roles = nameof(RoleEntry.SystemAdministrator))]
public class MaintenanceFacade : IMaintenanceFacade
{
	private readonly ICacheService _cacheService;

	public MaintenanceFacade(ICacheService cacheService)
	{
		_cacheService = cacheService;
	}

	public Task ClearCache(CancellationToken cancellationToken = default)
	{
		_cacheService.Clear();

		return Task.CompletedTask;
	}
}
