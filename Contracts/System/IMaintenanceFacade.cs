using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.System;

[ApiContract]
public interface IMaintenanceFacade
{
	Task ClearCache(CancellationToken cancellationToken = default);
}
