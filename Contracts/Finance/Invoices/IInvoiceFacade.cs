using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.GoranG3.Contracts.Finance.Invoices
{
	[ServiceContract]
	public interface IInvoiceFacade
	{
		Task<GetInvoicesResult> GetInvoices(GetInvoicesRequest request, CancellationToken cancellationToken = default);
	}
}