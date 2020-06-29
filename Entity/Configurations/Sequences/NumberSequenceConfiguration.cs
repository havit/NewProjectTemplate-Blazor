using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.GoranG3.Model.Metadata.Sequences;
using Havit.GoranG3.Model.Sequences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havit.GoranG3.Entity.Configurations.Sequences
{
	public class NumberSequenceConfiguration : IEntityTypeConfiguration<NumberSequence>
	{
		public void Configure(EntityTypeBuilder<NumberSequence> builder)
		{
			builder.HasIndex(NumberSequence => NumberSequence.MigrationId).IsUnique();
		}
	}
}