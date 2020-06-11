using Havit.GoranG3.Model.Attrida;
using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.Security;
using Havit.Model.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.HumanResources
{
	/// <summary>
	/// G2: Pracovnik
	/// </summary>
	public class Employee
	{
		public int Id { get; set; }

		public User User { get; set; }
		public int? UserId { get; set; }

		/// <summary>
		/// Team, where the employee is the one and only member.
		/// </summary>
		public Team PrivateTeam { get; private set; }
		public int PrivateTeamId { get; private set; }

		/// <summary>
		/// Title/degree in front of name
		/// G2: TitulyPredJmenem
		/// </summary>
		[MaxLength(20)]
		public string TitlePrefix { get; set; }

		/// <summary>
		/// G2: KrestniJmeno
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string FirstName
		{
			get => _firstName;
			set
			{
				_firstName = value;
				UpdateNames();
			}
		}
		private string _firstName;

		/// <summary>
		/// G2: Prijmeni
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string LastName
		{
			get => _lastName;
			set
			{
				_lastName = value;
				UpdateNames();
			}
		}
		private string _lastName;

		/// <summary>
		/// Title/degree after the name
		/// G2: TitulyZaJmenem
		/// </summary>
		[MaxLength(20)]
		public string TitleSuffix { get; set; }

		public Contact Contact { get; set; }
		public int? ContactId { get; set; }

		/// <summary>
		/// Direct superior.
		/// </summary>
		public Employee Boss { get; set; }
		public int? BossId { get; set; }

		/// <summary>
		/// G2: RodneCislo
		/// </summary>
		[MaxLength(15)]
		public string BirthNumber { get; set; }
		public DateTime? BirthDate { get; set; }

		/// <summary>
		/// G2: DatumNastupu
		/// </summary>
		public DateTime? CooperationStartDate { get; set; }

		/// <summary>
		/// G2: DatumOdchodu
		/// </summary>
		public DateTime? CooperationEndDate { get; set; }

		[MaxLength]
		public string Note { get; set; }

		/// <summary>
		/// G2: PracovniVolnoAutomatickySchvalit
		/// </summary>
		public bool AbsencesAutoApproved { get; set; }

		/// <summary>
		/// G2: TimesheetyAutomatickySchvalit
		/// </summary>
		public bool TimesheetsAutoApproved { get; set; }

		/// <summary>
		/// G2: TimesheetNotifierPosilat
		/// </summary>
		public bool TimesheetNotificationsEnabled { get; set; }

		public bool IsActive { get; set; } = true;

		public AttridaObject AttridaObject { get; set; }
		public int? AttridaObjectId { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }

		public List<EmployeeHistory> HistoriesIncludingDeleted { get; } = new List<EmployeeHistory>();
		[NotMapped]
		public FilteringCollection<EmployeeHistory> Histories { get; }

		public List<TeamMembership> TeamMemberships { get; } = new List<TeamMembership>();

		public string FullName => (this.TitlePrefix + " " + this.FirstName + " " + this.LastName + ", " + this.TitleSuffix).Trim(' ', ',');
		public string Initials => ((!String.IsNullOrWhiteSpace(FirstName)) ? FirstName.Substring(0, 1) : String.Empty) + LastName.Substring(0, 1);
		public string DisplayAs => (this.LastName + " " + this.FirstName).Trim();

		public Employee(string firstName, string lastName) : this(isNew: true)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		/// <summary>
		/// Ctor used by EF Core when loading existing entity.
		/// </summary>
		private Employee() : this(isNew: false)
		{
		}

		private Employee(bool isNew)
		{
			this.Histories = new FilteringCollection<EmployeeHistory>(this.HistoriesIncludingDeleted, h => h.Deleted is null);

			if (isNew)
			{
				var privateTeamMembership = new TeamMembership() { Employee = this };
				this.PrivateTeam = new Team()
				{
					IsPrivateTeam = true,
					IsSystemTeam = true,
					IsActive = true,
					TeamMemberships = { privateTeamMembership },
				};
				privateTeamMembership.Team = this.PrivateTeam;
				this.TeamMemberships.Add(privateTeamMembership);

				var everyoneTeamMembership = new TeamMembership() { Employee = this, TeamId = (int)Team.Entry.Everyone };
				this.TeamMemberships.Add(everyoneTeamMembership);
			}
		}

		private void UpdateNames()
		{
			this.PrivateTeam.Name = this.DisplayAs;
			if (this.User != null)
			{
				this.User.DisplayName = this.DisplayAs;
			}
			if (this.Contact != null)
			{
				this.Contact.Name = this.DisplayAs;
			}
		}
	}
}
