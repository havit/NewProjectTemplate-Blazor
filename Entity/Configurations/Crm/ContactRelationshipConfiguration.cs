using Havit.GoranG3.Model.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Entity.Configurations.Crm
{
	public class ContactRelationshipConfiguration : IEntityTypeConfiguration<ContactRelationship>
	{
		public void Configure(EntityTypeBuilder<ContactRelationship> builder)
		{
			builder.HasIndex(contactRelationship => new { contactRelationship.ParentContactId, contactRelationship.DetailContactId }).IsUnique();
		}
	}
}