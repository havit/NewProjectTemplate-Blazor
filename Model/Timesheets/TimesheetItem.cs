using Havit.GoranG3.Model.HumanResources;
using Havit.GoranG3.Model.Projects;
using Havit.GoranG3.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Timesheets
{
	public class TimesheetItem
	{
		public int Id { get; set; }

		/// <summary>
		/// G2: Pracovnik
		/// </summary>
		public Employee Employee { get; set; }
		public int EmployeeId { get; set; }

		public DateTime Date { get; set; }

		public Project Project { get; set; }
		public int ProjectId { get; set; }

		/// <summary>
		/// G2: Faze
		/// </summary>
		public ProjectPhase ProjectPhase { get; set; }
		public int? ProjectPhaseId { get; set; }

		/// <summary>
		/// G2: PocetHodin
		/// </summary>
		[Column(TypeName = "decimal(9, 5)")]
		public decimal DurationHours { get; set; }

		/// <summary>
		/// G2: OsobniNaklady
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? PersonalCosts { get; set; }

		/// <summary>
		/// G2: RezijniPrirazkaVuciOsobnimNakladum
		/// </summary>
		[Column(TypeName = "Money")]
		public decimal? OverheadCosts { get; set; }

		public TimesheetItemCategory TimesheetItemCategory { get; set; }
		public int? TimesheetItemCategoryId { get; set; }

		[MaxLength(100)]
		public string Text { get; set; }

		/// <summary>
		/// G2: Schvalil (Pracovnik => User!)
		/// </summary>
		public User ApprovedBy { get; set; }
		public int? ApprovedById { get; set; }

		/// <summary>
		/// Indicates approval (not null).
		/// G2: SchvalenoKdy
		/// </summary>
		public DateTime? ApprovedAt { get; set; }

		/// <summary>
		/// G2: ExternalWorkItemId
		/// </summary>
		public int? ExternalId { get; set; }

		/// <summary>
		/// G2: TfsUpdatePending
		/// </summary>
		public bool ExternalUpdatePending { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }

		public bool IsApproved => (ApprovedAt != null);
	}
}
