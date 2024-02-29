using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Havit.NewProjectTemplate.Web.Client.Shared;

public partial class LoginDisplay : IDisposable
{
	[Parameter] public bool ShowOnlyInitials { get; set; }

	[Inject] protected NavigationManager NavigationManager { get; set; }

	private string currentUrl;

	// TODO: Dořešit <form> vs. HxDropdownMenu/HxDropdownItem
	protected override void OnInitialized()
	{
		currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
		NavigationManager.LocationChanged += OnLocationChanged;
	}

	private void OnLocationChanged(object sender, LocationChangedEventArgs e)
	{
		currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
		StateHasChanged();
	}

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

	public void Dispose()
	{
		NavigationManager.LocationChanged -= OnLocationChanged;
	}
}
