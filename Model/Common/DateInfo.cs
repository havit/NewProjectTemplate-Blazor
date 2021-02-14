using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Model.Common
{
	public class DateInfo
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public bool IsHoliday { get; set; } = true;

		[MaxLength(50)]
		public string Description { get; set; }
	}
}
