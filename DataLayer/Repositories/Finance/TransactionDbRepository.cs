using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataLoaders;
using Havit.GoranG3.Model.Finance;

namespace Havit.GoranG3.DataLayer.Repositories.Finance
{
	public partial class TransactionDbRepository : ITransactionRepository
	{
		protected override IEnumerable<Expression<Func<Transaction, object>>> GetLoadReferences()
		{
			yield return (Transaction t) => t.Items;
			yield return (Transaction t) => t.Payments;
		}
	}
}