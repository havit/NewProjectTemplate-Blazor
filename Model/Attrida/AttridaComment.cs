using Havit.NewProjectTemplate.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Model.Attrida
{
	/// <summary>
	/// G2: Zapisek
	/// </summary>
	public class AttridaComment
	{
		public int Id { get; set; }

		public AttridaObject AttridaObject { get; set; }
		public int AttridaObjectId { get; set; }

		[Required]
		[MaxLength]
		public string Text { get; set; }

		public User CreatedBy { get; set; }
		public int CreatedById { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }
	}
}
