using Havit.Data.EntityFrameworkCore.Metadata;
using Havit.NewProjectTemplate.Model.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havit.NewProjectTemplate.Entity.Configurations.Infrastructure;

public class DistributedCacheEntryConfiguration : IEntityTypeConfiguration<DistributedCacheEntry>
{
	public void Configure(EntityTypeBuilder<DistributedCacheEntry> builder)
	{
		// DistributedCacheEntry is not an application entity, do not generate data source, repository, etc.
		builder.HasAnnotation(ApplicationEntityAnnotationConstants.IsApplicationEntityAnnotationName, false);
	}
}
