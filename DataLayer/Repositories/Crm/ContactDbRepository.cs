using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataLoaders;
using Havit.GoranG3.Model.Crm;

namespace Havit.GoranG3.DataLayer.Repositories.Crm
{
	public partial class ContactDbRepository : IContactRepository
	{
		public List<Contact> GetAllIncludingDeleted()
		{
			return DataWithDeleted.Include(GetLoadReferences).ToList();
		}
	}
}