using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Security;
using Havit.NewProjectTemplate.Model.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Users
{
	[Service]
	public class G2UserMigrator : IG2UserMigrator
	{
		private readonly MigrationOptions options;
		private readonly IUserRepository userRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2UserMigrator(
			IOptions<MigrationOptions> options,
			IUserRepository userRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.userRepository = userRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateUsers()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM Uzivatel", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var users = userRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var userID = reader.GetValue<int>("UzivatelID");
				Console.Write($"Uzivatel => User: {userID}");
				var user = users.Find(p => p.MigrationId == userID);
				if (user == null)
				{
					Console.WriteLine(" INSERT");
					user = new User();
					user.MigrationId = userID;
					users.Add(user);
					unitOfWork.AddForInsert(user);
				}
				else
				{
					Console.WriteLine(" UPDATE");
					unitOfWork.AddForUpdate(user);
				}

				user.Username = reader.GetValue<string>("Username");
				user.Email = reader.GetValue<string>("Email");
				user.DisplayName = reader.GetValue<string>("DisplayAs");
				user.Disabled = reader.GetValue<bool>("Disabled");
				user.Created = reader.GetValue<DateTime>("Created");
				user.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}
