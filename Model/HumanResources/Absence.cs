using Havit.GoranG3.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.HumanResources
{
    /// <summary>
	/// G2: PracovniVolno
	/// </summary>
	public class Absence
    {
		public int Id { get; set; }

		/// <summary>
		/// G2: Pracovnik
		/// </summary>
		public Employee Employee { get; set; }
		public int EmployeeId { get; set; }

		/// <summary>
		/// G2: Datum
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// G2: DatumDo
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// G2: PohybDnu
		/// </summary>
		public Decimal Days { get; set; }

		/// <summary>
		/// G2: TypPracovnihoVolna
		/// </summary>
		public AbsenceType AbsenceType { get; set; }
		public int AbsenceTypeId { get; set; }

		[MaxLength(100)]
		public string Description { get; set; }

		/// <summary>
		/// G2: Schvalil (Pracovnik => User!)
		/// </summary>
		public User ApprovedBy { get; set; }
		public int? ApprovedById { get; set; }

		public DateTime? ApprovedAt { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
