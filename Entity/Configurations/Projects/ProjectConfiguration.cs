using Havit.GoranG3.Model.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Entity.Configurations.Projects
{
	public class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.Property(project => project.Id).ValueGeneratedNever().HasDefaultValueSql("NEXT VALUE FOR ProjectSequence");
			builder.Ignore(project => project.AllChildrenAndMe);
			builder.Ignore(project => project.AllParentsAndMe);
			builder.Ignore(project => project.PaymentDueDaysDefaultEffective);
			builder.Ignore(project => project.Children);
		}
	}
}
