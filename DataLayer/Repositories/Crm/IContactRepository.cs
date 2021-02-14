using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.Patterns.Repositories;
using Havit.NewProjectTemplate.Contracts.Crm;
using Havit.NewProjectTemplate.Model.Crm;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Crm
{
	public partial interface IContactRepository
	{
		public List<Contact> GetAllIncludingDeleted();
		Task<List<ContactReferenceVM>> GetAllContactReferencesAsync(CancellationToken cancellationToken = default);
	}
}