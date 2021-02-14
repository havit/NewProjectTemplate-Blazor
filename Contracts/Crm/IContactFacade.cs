using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.Crm
{
	[ServiceContract]
	public interface IContactFacade
	{
		public Task<Dto<List<ContactReferenceVM>>> GetAllContactReferencesAsync(CancellationToken cancellationToken = default);
	}
}
