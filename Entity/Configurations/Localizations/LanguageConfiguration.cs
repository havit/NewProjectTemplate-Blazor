using Havit.GoranG3.Model.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Entity.Configurations.Localizations
{
	public class LanguageConfiguration : IEntityTypeConfiguration<Language>
	{
		public void Configure(EntityTypeBuilder<Language> builder)
		{
			builder.Property(l => l.Id).ValueGeneratedNever();
		}
	}
}
