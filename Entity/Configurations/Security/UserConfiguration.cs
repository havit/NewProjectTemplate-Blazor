//using Havit.NewProjectTemplate.Model.Metadata.Security;
using Havit.NewProjectTemplate.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havit.NewProjectTemplate.Entity.Configurations.Security;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(user => user.NormalizedUsername).HasFilter("Deleted IS NULL").IsUnique();
		builder.HasIndex(user => user.NormalizedEmail).HasFilter("Deleted IS NULL").IsUnique();
		//builder.Property(user => user.Username).HasColumnType($"nvarchar({UserMetadata.UsernameMaxLength}) COLLATE Latin1_General_CI_AI");
		//builder.Property(user => user.Email).HasColumnType($"nvarchar({UserMetadata.EmailMaxLength}) COLLATE Latin1_General_CI_AI");
	}
}
