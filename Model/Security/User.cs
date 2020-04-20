using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Security
{
    public class User
	{
		public int Id { get; set; }

		[MaxLength(50)]
		public string Username { get; set; }

		/// <summary>
		/// Normalized Username = UpperCase
		/// </summary>
		[MaxLength(50)]
		public string NormalizedUsername { get; set; }

		[MaxLength(255)]
		public string Email { get; set; }

		/// <summary>
		/// Normalized Email = UpperCase
		/// </summary>
		[MaxLength(255)]
		public string NormalizedEmail { get; set; }

		public bool EmailConfirmed { get; set; }

		[MaxLength(Int32.MaxValue)]
		public string PasswordHash { get; set; }

		/// <summary>
		/// Gets or sets the date and time, in UTC, when any user lockout ends.
		/// </summary>
		/// <remarks>
		/// A value in the past means the user is not locked out.
		/// </remarks>
		public DateTimeOffset? LockoutEnd { get; set; }

		/// <summary>
		/// Gets or sets a flag indicating if the user could be locked out.
		/// </summary>
		public bool LockoutEnabled { get; set; }

		/// <summary>
		/// Gets or sets the number of failed login attempts for the current user.
		/// </summary>
		public int AccessFailedCount { get; set; }

		/// <summary>
		/// A random value that must change whenever a users credentials change (password changed, login removed)
		/// </summary>
		[MaxLength(255)] // GUID
		public virtual string SecurityStamp { get; set; }

		public List<UserRole> UserRoles { get; } = new List<UserRole>();

		public DateTime Created { get; set; }

		public DateTime? Deleted { get; set; }

		public bool IsInRole(Role.Entry roleEntry)
		{
			return UserRoles.Any(ur => ur.RoleId == (int)roleEntry);
		}
	}
}
