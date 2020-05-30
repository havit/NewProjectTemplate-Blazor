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
	public class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder.Property(c => c.Id).HasDefaultValueSql("NEXT VALUE FOR ContactSequence");
			builder.HasMany(c => c.DetailContactRelationships).WithOne(cr => cr.ParentContact);
			builder.HasMany(c => c.ParentContactRelationships).WithOne(cr => cr.DetailContact);
		}
	}
}
