using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Shared;

public partial class LoginDisplay
{
	[Parameter] public bool ShowOnlyInitials { get; set; }

	/// <summary>
	/// Takes an email or name of the user and returns the user's initials.
	/// </summary>
	/// <returns>The initials of first and last name</returns>
	private string NameToInitials(string name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			return null;
		}

		if (name.Contains('@'))
		{
			var mail = name.Split('@')[0].Split('.');
			if (mail.Length == 1)
			{
				return mail[0][0].ToString().ToUpper();
			}

			return (mail[0][0].ToString() + mail[^1][0].ToString()).ToUpper();
		}

		var names = name.Split(' ');
		if (names.Length == 1)
		{
			return names[0][0].ToString().ToUpper();
		}

		return (names[0][0].ToString() + names[^1][0].ToString()).ToUpper();
	}
}
