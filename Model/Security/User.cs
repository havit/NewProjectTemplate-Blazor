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

		[MaxLength(255)]
		public string Email { get; set; }

		public bool EmailConfirmed { get; set; }

		[MaxLength(Int32.MaxValue)]
		public string PasswordHash { get; set; }
	}
}
