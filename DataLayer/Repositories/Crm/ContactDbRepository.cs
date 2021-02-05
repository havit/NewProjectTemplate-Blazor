using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataLoaders;
using Havit.GoranG3.Contracts.Crm;
using Havit.GoranG3.Model.Crm;
using Microsoft.EntityFrameworkCore;

namespace Havit.GoranG3.DataLayer.Repositories.Crm
{
	public partial class ContactDbRepository : IContactRepository
	{
		public List<Contact> GetAllIncludingDeleted()
		{
			return DataIncludingDeleted.Include(GetLoadReferences).ToList();
		}

		public async Task<List<ContactReferenceVM>> GetAllContactReferencesAsync(CancellationToken cancellationToken = default)
		{
			return await DataIncludingDeleted.Select(c => new ContactReferenceVM() { Id = c.Id, Name = c.Name, IsDeleted = (c.Deleted != null) }).ToListAsync(cancellationToken);
		}
	}
}