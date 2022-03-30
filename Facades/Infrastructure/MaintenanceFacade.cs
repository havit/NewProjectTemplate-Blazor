using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Havit.NewProjectTemplate.Model.Security;
using Havit.Services.Caching;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure;

[Service]
[Authorize(Roles = nameof(Role.Entry.SystemAdministrator))]
public class MaintenanceFacade : IMaintenanceFacade
{
	private readonly ICacheService cacheService;

	public MaintenanceFacade(ICacheService cacheService)
	{
		this.cacheService = cacheService;
	}

	public Task ClearCache(CancellationToken cancellationToken = default)
	{
		cacheService.Clear();

		return Task.CompletedTask;
	}
}
