using System.ComponentModel.DataAnnotations;

namespace Havit.NewProjectTemplate.Model.Infrastructure;

/// <summary>
/// To be used by Microsoft.Extensions.Caching.SqlServer.
/// </summary>
public class DistributedCacheEntry
{
	[MaxLength(100)]
	public string Id { get; set; }
	public byte[] Value { get; set; }
	public DateTimeOffset ExpiresAtTime { get; set; }
	public long? SlidingExpirationInSeconds { get; set; }
	public DateTimeOffset? AbsoluteExpiration { get; set; }
}