using Havit.Data.EntityFrameworkCore.Attributes;
using Havit.Model.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Crm
{
	/// <summary>
	/// Person or organization.
	/// G2: Subjekt
	/// </summary>
	[Cache]
	public class Contact // TODO Tags (G2: KategorieSubjektu)
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		/// <summary>
		/// Main address (registered address for organizations, permanent address for persons)
		/// G2: HlavniAdresa
		/// </summary>
		public Address RegisteredAddress { get; set; }
		public int? RegisteredAddressId { get; set; }

		/// <summary>
		/// Set only if different from RegisteredAddress.
		/// G2: Adresy.Where(??)
		/// </summary>
		public Address ContactAddress { get; set; }
		public int? ContactAddressId { get; set; }

		/// <summary>
		/// G2: SpojeniSubjektu[Telefon]
		/// </summary>
		[MaxLength(20)]
		public string Phone { get; set; }

		/// <summary>
		/// G2: SpojeniSubjektu[Mobil]
		/// </summary>
		[MaxLength(20)]
		public string Mobile { get; set; }

		/// <summary>
		/// G2: SpojeniSubjektu[Email]
		/// </summary>
		[MaxLength(200)]
		public string Email { get; set; }

		/// <summary>
		/// G2: SpojeniSubjektu[Web]
		/// </summary>
		[MaxLength(200)]
		public string Web { get; set; }

		/// <summary>
		/// G2: Ico
		/// </summary>
		[MaxLength(15)]
		public string CompanyRegistrationNumber { get; set; }

		/// <summary>
		/// G2: Dic
		/// </summary>
		/// <remarks>
		/// Includes VAT Identification Number. Might be separated later on (SK, ...).
		/// </remarks>
		[MaxLength(15)]
		public string TaxRegistrationNumber { get; set; }

		/// <summary>
		/// Company registered for VAT.
		/// G2: PlatceDph
		/// </summary>
		public bool IsVatPayer { get; set; }

		/// <summary>
		/// Registration to Business Register or similar register
		/// G2: ZapisDoRejstriku
		/// </summary>
		[MaxLength(100)]
		public string CertificateOfIncorporation { get; set; }

		/// <summary>
		/// G2: BankovniSpojeni
		/// </summary>
		[MaxLength(400)]
		public string BankName { get; set; }

		/// <summary>
		/// G2: CisloUctuZaklad + "/" + CisloUctuKodBanky
		/// </summary>
		[MaxLength(50)]
		public string BankAccountNumber { get; set; }

		/// <summary>
		/// G2: CisloUctuIban
		/// </summary>
		[MaxLength(40)]
		public string BankAccountIban { get; set; }

		/// <summary>
		/// G2: CisloUctuSwiftBic
		/// </summary>
		[MaxLength(11)]
		public string BankAccountSwiftBic { get; set; }

		[MaxLength]
		public string Note { get; set; }

		/// <summary>
		/// G2: IsArchivni
		/// </summary>
		public bool IsArchived { get; set; }

		/// <summary>
		/// G2: ZakladniSubjekt
		/// </summary>
		public bool IsBasicContact { get; set; } = true;

		[Required]
		[MaxLength(50)]
		public string ExternalCode { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		/// <summary>
		/// Indicates business partner where we issue them invoice with no VAT.
		/// G2: FakturyVystaveneBezDph
		/// </summary>
		public bool HasNoVatForInvoicesIssued { get; set; }

		/// <summary>
		/// Relationships where this contact is detail (= list of parents).
		/// </summary>
		public List<ContactRelationship> ParentContactRelationships { get; } = new List<ContactRelationship>();
		/// <summary>
		/// Relationships where this contact is parent (= list of details).
		/// </summary>
		public List<ContactRelationship> DetailContactRelationships { get; } = new List<ContactRelationship>();
	}
}
