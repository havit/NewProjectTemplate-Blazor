using Havit.GoranG3.Model.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Entity.Configurations.HumanResources
{
	public class TeamConfiguration : IEntityTypeConfiguration<Team>
	{
		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.Property(team => team.Id).HasDefaultValueSql("NEXT VALUE FOR TeamSequence");
		}
	}
}
