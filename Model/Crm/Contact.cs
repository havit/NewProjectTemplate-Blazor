using Havit.Data.EntityFrameworkCore.Attributes;
using Havit.NewProjectTemplate.Model.Attrida;
using Havit.Model.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Model.Crm
{
	/// <summary>
	/// Person or organization.
	/// G2: Subjekt
	/// </summary>
	[Cache]
	public class Contact
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		/// <summary>
		/// Main address (registered address for organizations, permanent address for persons)
		/// </summary>
		public Address RegisteredAddress { get; set; }
		public int? RegisteredAddressId { get; set; }

		/// <summary>
		/// Set only if different from RegisteredAddress.
		/// </summary>
		public Address ContactAddress { get; set; }
		public int? ContactAddressId { get; set; }

		[MaxLength(20)]
		public string Phone { get; set; }

		[MaxLength(20)]
		public string Mobile { get; set; }

		[MaxLength(200)]
		public string Email { get; set; }

		[MaxLength(200)]
		public string Web { get; set; }

		/// <summary>
		/// CZ: IČ
		/// </summary>
		[MaxLength(15)]
		public string CompanyRegistrationNumber { get; set; }

		/// <summary>
		/// CZ: DIČ
		/// </summary>
		/// <remarks>
		/// Includes VAT Identification Number. Might be separated later on (SK, ...).
		/// </remarks>
		[MaxLength(15)]
		public string TaxRegistrationNumber { get; set; }

		public AttridaObject AttridaObject { get; set; }
		public int? AttridaObjectId { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public enum Entry
		{
			Self = -1
		}
	}
}
