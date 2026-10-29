using Havit.NewProjectTemplate.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havit.NewProjectTemplate.Entity.Configurations.Security;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(user => user.IdentityProviderExternalId).HasFilter("Deleted IS NULL").IsUnique();
	}
}
