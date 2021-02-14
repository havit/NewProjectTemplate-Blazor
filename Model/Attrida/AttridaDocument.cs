using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Model.Attrida
{
	/// <summary>
	/// G2: Document, DocumentRelation
	/// </summary>
	public class AttridaDocument
	{
		public int Id { get; set; }

		public AttridaObject AttridaObject { get; set; }
		public int AttridaObjectId { get; set; }

		[Required]
		[MaxLength(200)]
		public string OriginalFilename { get; set; }

		[Required]
		[MaxLength(200)]
		public string StorageFilename { get; set; }

		public FileType FileType { get; set; }

		[MaxLength(100)]
		public string Description { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }
	}
}
