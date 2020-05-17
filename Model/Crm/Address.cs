using System.ComponentModel.DataAnnotations;

namespace Havit.GoranG3.Model.Crm
{
	public class Address
	{
		public int Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; } // TODO Pojmenování property?

		[Required]
		[MaxLength(200)]
		public string Street { get; set; }

		[Required]
		[MaxLength(200)]
		public string City { get; set; }

		[Required]
		[MaxLength(20)]
		public string Psc { get; set; }

		public Country Country { get; set; }
		public int? CountryId { get; set; }
	}
}