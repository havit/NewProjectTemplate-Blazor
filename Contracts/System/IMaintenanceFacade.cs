using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.System
{
	[ServiceContract]
	public interface IMaintenanceFacade
	{
		Task ClearCache(CancellationToken cancellationToken = default);
	}
}