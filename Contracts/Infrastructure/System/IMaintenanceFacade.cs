using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.Infrastructure.System;

[ApiContract]
public interface IMaintenanceFacade
{
	Task ClearCache(CancellationToken cancellationToken = default);
}
