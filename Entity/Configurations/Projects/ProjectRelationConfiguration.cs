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
	public class ProjektRelationConfiguration : IEntityTypeConfiguration<ProjectRelation>
	{
		public void Configure(EntityTypeBuilder<ProjectRelation> builder)
		{
			builder.HasOne(projectRelation => projectRelation.HigherProject)
				.WithMany(project => project.AllChildrenAndMeRelations)
				.HasForeignKey(projectRelation => projectRelation.HigherProjectId)
				.IsRequired();

			builder.HasOne(projectRelation => projectRelation.LowerProject)
				.WithMany(project => project.AllParentsAndMeRelations)
				.HasForeignKey(projectRelation => projectRelation.LowerProjectId)
				.IsRequired();
		}
	}
}
