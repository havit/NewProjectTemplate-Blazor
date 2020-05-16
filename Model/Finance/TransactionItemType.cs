using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	/// <summary>
	/// G2: TypFakturaItem
	/// </summary>
	[Cache(Priority = CacheItemPriority.High)]
	public class TransactionItemType // TODO
    {
		public int Id { get; set; }

		public enum Entry // TODO
		{
			Revenue = -1,
			Cost = -2
		}
	}
}
