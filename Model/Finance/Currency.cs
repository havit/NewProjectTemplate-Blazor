using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Finance
{
	[Cache(Priority = CacheItemPriority.High)]
	public class Currency // TODO
	{
		public int Id { get; set; }
	}
}
