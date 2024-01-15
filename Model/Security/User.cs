using System.ComponentModel.DataAnnotations;
using Havit.NewProjectTemplate.Primitives.Security;

namespace Havit.NewProjectTemplate.Model.Security;

public class User
{
	public int Id { get; set; }

	[MaxLength(100)]
	public string DisplayName { get; set; }

	[MaxLength(255)]
	public string Email { get; set; }

	/// <summary>
	/// Identifier of the user with external Identity Provider.
	/// </summary>
	[MaxLength(255)]
	public string IdentityProviderExternalId { get; set; }

	public List<UserRole> UserRoles { get; } = new List<UserRole>();

	public bool Disabled { get; set; } = false;

	public DateTime Created { get; set; }
	public DateTime? Deleted { get; set; }

	public bool IsInRole(RoleEntry roleEntry)
	{
		return UserRoles.Any(ur => ur.RoleId == (int)roleEntry);
	}
}
