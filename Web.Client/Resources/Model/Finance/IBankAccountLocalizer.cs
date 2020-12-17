using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public interface IBankAccountLocalizer
	{
		string this[string name] { get; }

		string AccountNumber { get; }
		string BankName { get; }
		string Delete { get; }
		string DeleteConfirmation { get; }
		string DeleteSuccess { get; }
		string Iban { get; }
		string Name { get; }
		string New { get; }
		string NewSuccess { get; }
		string Plural { get; }
		string Singular { get; }
		string SwiftBic { get; }
		string UpdateSuccess { get; }

		IEnumerable<LocalizedString> GetAllStrings();
		IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures = false);
		string GetString(string name);
		string GetString(string name, params object[] arguments);
	}
}