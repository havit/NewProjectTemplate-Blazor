using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Pages.Admin
{
	public interface IAdminIndexLocalizer : IStringLocalizer<AdminIndexLocalizer>
	{
		LocalizedString CultureRemoved { get; }
	}
}