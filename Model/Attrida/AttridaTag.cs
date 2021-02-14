using System.ComponentModel.DataAnnotations;

namespace Havit.NewProjectTemplate.Model.Attrida
{
	public class AttridaTag
	{
		public int Id { get; set; }

		public AttridaObject AttridaObject { get; set; }
		public int AttridaObjectId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}
}