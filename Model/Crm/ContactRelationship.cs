using System.ComponentModel.DataAnnotations;

namespace Havit.GoranG3.Model.Crm
{
	public class ContactRelationship
	{
		public int Id { get; set; }

		public ContactRelationshipType RelationshipType { get; set; }

		public Contact ParentContact { get; set; }
		public int ParentContactId { get; set; }

		public Contact DetailContact { get; set; }
		public int DetailContactId { get; set; }

		[MaxLength(100)]
		public string Description { get; set; }
	}
}