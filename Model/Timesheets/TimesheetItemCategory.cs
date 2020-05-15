using Havit.Data.EntityFrameworkCore.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Havit.GoranG3.Model.Timesheets
{
	[Cache]
	public class TimesheetItemCategory
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: TimesheetItemCategoryLocalization.Nazev
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}