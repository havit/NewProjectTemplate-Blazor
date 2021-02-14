using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Contracts.Crm;
using Havit.NewProjectTemplate.DataLayer.Repositories.Crm;

namespace Havit.NewProjectTemplate.Facades.Crm
{
	public class ContactFacade : IContactFacade
	{
		private readonly IContactRepository contactRepository;

		public ContactFacade(IContactRepository contactRepository)
		{
			this.contactRepository = contactRepository;
		}

		public async Task<Dto<List<ContactReferenceVM>>> GetAllContactReferencesAsync(CancellationToken cancellationToken = default)
		{
			return Dto.FromValue(await contactRepository.GetAllContactReferencesAsync(cancellationToken));
		}
	}
}
