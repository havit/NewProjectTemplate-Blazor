using Havit.ComponentModel;

namespace Havit.NewProjectTemplate.Contracts.Infrastructure;

[ApiContract]
public interface IMaintenanceFacade
{
	Task ClearCacheAsync(CancellationToken cancellationToken = default);
}
