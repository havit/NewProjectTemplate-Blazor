using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public interface IExchangeRateLocalizer
	{
		LocalizedString DateFrom { get; }
		LocalizedString Plural { get; }
		LocalizedString Rate { get; }
		LocalizedString Singular { get; }
		LocalizedString Currency { get; }
	}
}