using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Security;
using Havit.Services.Caching;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure;

[Service]
[Authorize(Roles = nameof(RoleEntry.SystemAdministrator))]
public class MaintenanceFacade(ICacheService _cacheService) : IMaintenanceFacade
{
	public Task ClearCacheAsync(CancellationToken cancellationToken = default)
	{
		_cacheService.Clear();

		return Task.CompletedTask;
	}
}
