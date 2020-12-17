using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	/// <summary>
	/// Marker file for IStringLocalizer<>
	/// </summary>
	public class BankAccountLocalizer : IBankAccountLocalizer
	{
		private readonly IStringLocalizer<BankAccountLocalizer> stringLocalizer;

		public BankAccountLocalizer(IStringLocalizer<BankAccountLocalizer> stringLocalizer)
		{
			this.stringLocalizer = stringLocalizer;
		}

		// string-API properties
		public string AccountNumber => this["AccountNumber"];
		public string BankName => this["BankName"];
		public string Iban => this["Iban"];
		public string Name => this["Name"];
		public string SwiftBic => this["SwiftBic"];
		public string New => this["New"];
		public string Plural => this["Plural"];
		public string Singular => this["Singular"];
		public string Delete => this["Delete"];
		public string DeleteConfirmation => this["DeleteConfirmation"];
		public string DeleteSuccess => this["DeleteSuccess"];
		public string NewSuccess => this["NewSuccess"];
		public string UpdateSuccess => this["UpdateSuccess"];

		public string this[string name] => stringLocalizer[name];

		// StringLocalizer re-publishing
		// TODO create StrongApiLocalizerBase base-class?!
		public string GetString(string name) => stringLocalizer.GetString(name);
		public string GetString(string name, params object[] arguments) => stringLocalizer.GetString(name, arguments);
		public IEnumerable<LocalizedString> GetAllStrings() => stringLocalizer.GetAllStrings();
		public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures = false) => stringLocalizer.GetAllStrings(includeParentCultures);

	}
}
