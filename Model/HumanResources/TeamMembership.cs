namespace Havit.GoranG3.Model.HumanResources
{
	/// <summary>
	/// G2: Team_Pracovnik
	/// </summary>
	public class TeamMembership
	{
		public int Id { get; set; }

		public Team Team { get; set; }
		public int TeamId { get; set; }

		public Employee Employee { get; set; }
		public int EmployeeId { get; set; }
	}
}