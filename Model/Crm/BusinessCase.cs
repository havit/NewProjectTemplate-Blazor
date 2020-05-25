using Havit.GoranG3.Model.Attrida;
using Havit.GoranG3.Model.Projects;
using Havit.GoranG3.Model.Security;
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
	/// Business opportunity / risk
	/// G2: ObchodniPrilezitost
	/// </summary>
	public class BusinessCase // TODO RH: Sjednotit s úkoly?
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: TypObchodniPrilezitosti
		/// </summary>
		public BusinessCaseType BusinessCaseType { get; set; }

		/// <summary>
		/// G2: Protistrana
		/// </summary>
		public Contact BusinessPartner { get; set; }
		public int? BusinessPartnerId { get; set; }

		public Project Project { get; set; }
		public int? ProjectId { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		/// <summary>
		/// Normalized probability [100% => 1.0m]
		/// </summary>
		[Column(TypeName = "decimal(5, 3)")]
		public decimal Probability { get; set; }

		/// <summary>
		/// G2: FinancniObjem
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? FinancialValue { get; set; }

		[Column(TypeName = "Date")]
		public DateTime? ReminderDate { get; set; }

		/// <summary>
		/// G2: AssignedTo (Pracovnik => User)
		/// </summary>
		public User AssignedTo { get; set; }
		public int AssignedToId { get; set; }

		/// <summary>
		/// G2: PredmetDodavky
		/// </summary>
		[MaxLength]
		public string Description { get; set; }

		/// <summary>
		/// G2: StavObchodniPrilezitostiId
		/// </summary>
		public BusinessCaseState State { get; set; }

		public AttridaObject AttridaObject { get; set; }
		public int? AttridaObjectId { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		/// <summary>
		/// G2: LastChangedByUzivatelId
		/// </summary>
		public User ModifiedBy { get; set; }
		public int? ModifiedById { get; set; }
	}
}
