using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Havit.GoranG3.Model.HumanResources
{
	public class Team
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// This is a single-employee team.
		/// </summary>
		public bool IsPrivateTeam { get; set; }

		/// <summary>
		/// Indicates system teams (private, Everyone, ...)
		/// </summary>
		public bool IsSystemTeam { get; set; }

		public bool IsActive { get; set; } = true;

		public List<TeamMembership> TeamMemberships { get; } = new List<TeamMembership>();

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		public int? MigrationId { get; set; }


		public IEnumerable<Employee> Members => TeamMemberships.Select(tm => tm.Employee);

		public enum Entry
		{
			Everyone = -1
		}
	}
}