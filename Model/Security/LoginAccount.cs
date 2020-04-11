using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Security
{
	public class LoginAccount
	{
		public int Id { get; set; }

		/// <summary>
		/// Identifikátor uživatele v externím systému.
		/// </summary>
		[MaxLength(32)]
		public string Username { get; set; }
	}
}
