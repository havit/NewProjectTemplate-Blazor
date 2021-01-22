using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public interface INavigationLocalizer
	{
		LocalizedString Administration { get; }
		LocalizedString BankAccounts { get; }
		LocalizedString Currencies { get; }
		LocalizedString ExchangeRates { get; }
	}
}