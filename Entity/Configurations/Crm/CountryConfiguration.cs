using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.GoranG3.Model.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havit.GoranG3.Entity.Configurations.Crm
{
	public class CountryConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.HasIndex(c => c.IsoCode).IsUnique();
			builder.HasIndex(c => c.IsoCode3).IsUnique();
		}
	}
}
