using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Crm;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.DataLayer.Repositories.Security;
using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.HumanResources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.HumanResources
{
	[Service]
	public class G2TeamMigrator : IG2TeamMigrator
	{
		private readonly MigrationOptions options;
		private readonly ITeamRepository teamRepository;
		private readonly ITeamMembershipRepository teamMembershipRepository;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2TeamMigrator(
			IOptions<MigrationOptions> options,
			ITeamRepository teamRepository,
			ITeamMembershipRepository teamMembershipRepository,
		IEmployeeRepository employeeRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.teamRepository = teamRepository;
			this.teamMembershipRepository = teamMembershipRepository;
			this.employeeRepository = employeeRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateTeams()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT te.TeamID, MAX(te.Nazev) AS Name, CASE WHEN EXISTS (SELECT * FROM Team t WHERE t.TeamID = te.TeamID AND t.PrivatniTeam = 1) THEN 1 ELSE 0 END AS IsPrivateTeam,CASE WHEN EXISTS (SELECT * FROM Team t WHERE t.TeamID = te.TeamID AND t.Aktivni = 1) THEN 1 ELSE 0 END AS IsActive, STRING_AGG (pr.PracovnikID, ',') AS TeamMembers FROM Team te LEFT JOIN Team_Pracovnik pr ON te.TeamID = pr.TeamID WHERE SystemovyTeam = 0 GROUP BY te.TeamID", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var teams = teamRepository.GetAllIncludingDeleted();
			var employees = employeeRepository.GetAllIncludingDeleted();
			var teamMemberships = teamMembershipRepository.GetAll();

			while (reader.Read())
			{
				var teamID = reader.GetValue<int>("TeamID");
				var team = teams.Find(e => e.MigrationId == teamID);
				Console.Write("Team: " + teamID);

				if (team == null)
				{
					team = new Team();
					team.MigrationId = teamID;
					unitOfWork.AddForInsert(team);
					Console.WriteLine(" INSERT");
				}
				else
				{
					Console.WriteLine(" UPDATE");
					unitOfWork.AddForUpdate(team);
				}

				team.Name = reader.GetValue<string>("Name");
				team.IsPrivateTeam = (reader.GetValue<int>("IsPrivateTeam") == 1);
				team.IsActive = (reader.GetValue<int>("IsActive") == 1);

				var teamMembers = reader.GetValue<string>("TeamMembers");

				if (!String.IsNullOrWhiteSpace(teamMembers))
				{
					var ids = teamMembers.Split(',').Select(Int32.Parse);

					foreach (var teamMemberId in ids)
					{
						var employee = employees.Find(e => e.MigrationId == teamMemberId);
						var teamMembership = teamMemberships.Find(m => (m.Employee == employee) && (m.Team == team));
						if ((employee != null) && (teamMembership != null))
						{
							TeamMembership newMembership = new TeamMembership();
							newMembership.Team = team;
							newMembership.Employee = employee;
							unitOfWork.AddForInsert(newMembership); // Is it neccessary?
						}
					}
				}
			}

			unitOfWork.Commit();
		}

	}
}
