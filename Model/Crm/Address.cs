using Havit.Data.EntityFrameworkCore.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Havit.GoranG3.Model.Crm
{
	/// <summary>
	/// G2: AdresaSubjektu
	/// </summary>
	[Cache(Priority = CacheItemPriority.Low)]
	public class Address
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: Nazev
		/// </summary>
		[MaxLength(50)]
		public string Line1 { get; set; }

		/// <summary>
		/// G2: Ulice
		/// </summary>
		[Required]
		[MaxLength(200)]
		public string Line2 { get; set; }

		[Required]
		[MaxLength(200)]
		public string City { get; set; }

		/// <summary>
		/// G2: Psc
		/// </summary>
		[Required]
		[MaxLength(20)]
		public string Zip { get; set; }

		public Country Country { get; set; }
		public int? CountryId { get; set; }

		public int? MigrationId { get; set; }

		public string InlineForm
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				if (!string.IsNullOrEmpty(this.Line1))
				{
					sb.Append(this.Line1);
					sb.Append(", ");
				}
				if (!string.IsNullOrEmpty(this.Line2))
				{
					sb.Append(this.Line2);
					sb.Append(", ");
				}
				if (!string.IsNullOrEmpty(this.City))
				{
					sb.Append(this.City);
					sb.Append(", ");
				}
				if (!string.IsNullOrEmpty(this.Zip))
				{
					sb.Append(this.Zip);
					sb.Append(", ");
				}
				if (this.Country != null)
				{
					sb.Append(this.Country.IsoCode);
					sb.Append(", ");
				}
				if (sb.Length > 0)
				{
					sb.Length = sb.Length - 2;
				}
				return sb.ToString();
			}
		}
	}
}