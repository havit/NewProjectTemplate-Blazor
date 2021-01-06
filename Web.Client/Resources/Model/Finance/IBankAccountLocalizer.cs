using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public interface IBankAccountLocalizer : IStringLocalizer<BankAccountLocalizer>
	{
		LocalizedString AccountNumber { get; }
		LocalizedString BankName { get; }
		LocalizedString Delete { get; }
		LocalizedString DeleteConfirmation { get; }
		LocalizedString Iban { get; }
		LocalizedString Name { get; }
		LocalizedString New { get; }
		LocalizedString Plural { get; }
		LocalizedString Singular { get; }
		LocalizedString SwiftBic { get; }
	}
}