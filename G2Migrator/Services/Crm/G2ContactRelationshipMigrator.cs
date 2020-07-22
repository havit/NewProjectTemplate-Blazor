using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Crm;
using Havit.GoranG3.Model.Crm;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.Crm
{
	[Service]
	public class G2ContactRelationshipMigrator : IG2ContactRelationshipMigrator
	{
		private readonly MigrationOptions options;
		private readonly IContactRelationshipRepository contactRelationshipRepository;
		private readonly IContactRepository contactRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2ContactRelationshipMigrator(
			IOptions<MigrationOptions> options,
			IContactRelationshipRepository contactRelationshipRepository,
			IContactRepository contactRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.contactRelationshipRepository = contactRelationshipRepository;
			this.contactRepository = contactRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateContactRelationships()
		{
			var contacts = contactRepository.GetAllIncludingDeleted();

			if (contacts.Any())
			{
				using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
				conn.Open();
				using SqlCommand cmd = new SqlCommand("SELECT * FROM VztahSubjektu", conn);
				using SqlDataReader reader = cmd.ExecuteReader();

				var relationships = contactRelationshipRepository.GetAll();

				while (reader.Read())
				{
					var contactRelationshipID = reader.GetValue<int>("VztahSubjektuID");
					var parentContact = contacts.Find(c => c.MigrationId == reader.GetValue<int>("ParentSubjektID"));
					var detailContact = contacts.Find(c => c.MigrationId == reader.GetValue<int>("DetailSubjektID"));
					Console.Write("VztahSubjektu => ContactRelationship: " + contactRelationshipID);
					var contactRelationship = relationships.Find(r => (r.ParentContact == parentContact) && (r.DetailContact == detailContact));

					if (contactRelationship == null)
					{
						contactRelationship = new ContactRelationship();
						unitOfWork.AddForInsert(contactRelationship);
						Console.WriteLine(" INSERT");
					}
					else
					{
						unitOfWork.AddForUpdate(contactRelationship);
						Console.WriteLine(" UPDATE");
					}

					contactRelationship.DetailContact = detailContact;
					contactRelationship.ParentContact = parentContact;
					contactRelationship.Description = reader.GetValue<string>("NazevDleTypu");
					contactRelationship.RelationshipType = ContactRelationshipType.ContactPerson;
				}

				unitOfWork.Commit();
			}
		}
	}
}
