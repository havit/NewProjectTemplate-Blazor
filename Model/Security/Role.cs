using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Security
{
    [Cache(Priority = CacheItemPriority.High)]
	public class Role
    {
		public int Id { get; set; }

		[MaxLength(255)]
		public string Name { get; set; }

		[MaxLength(255)]
		public string NormalizedName { get; set; }

		public enum Entry
		{
			SystemAdministrator = -1,
			UserSettingsAdministrator = -2
		}
	}
}
