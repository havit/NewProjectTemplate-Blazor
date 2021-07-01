using System.Threading;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Services.Jobs
{
	public interface IRunnableJob
	{
		Task ExecuteAsync(CancellationToken cancellationToken);
	}
}
