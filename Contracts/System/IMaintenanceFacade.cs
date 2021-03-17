using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.System
{
	public interface IMaintenanceFacade
	{
		Task ClearCache(CancellationToken cancellationToken = default);
	}
}