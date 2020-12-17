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
	public class BankAccountLocalizer : IBankAccountLocalizer, IStringLocalizer<BankAccountLocalizer>
	{
		private readonly IStringLocalizer<BankAccountLocalizer> stringLocalizer;

		public BankAccountLocalizer(IStringLocalizer<BankAccountLocalizer> stringLocalizer)
		{
			this.stringLocalizer = stringLocalizer;
		}

		// string-API properties
		public LocalizedString AccountNumber => this["AccountNumber"];
		public LocalizedString BankName => this["BankName"];
		public LocalizedString Iban => this["Iban"];
		public LocalizedString Name => this["Name"];
		public LocalizedString SwiftBic => this["SwiftBic"];
		public LocalizedString New => this["New"];
		public LocalizedString Plural => this["Plural"];
		public LocalizedString Singular => this["Singular"];
		public LocalizedString Delete => this["Delete"];
		public LocalizedString DeleteConfirmation => this["DeleteConfirmation"];
		public LocalizedString DeleteSuccess => this["DeleteSuccess"];
		public LocalizedString NewSuccess => this["NewSuccess"];
		public LocalizedString UpdateSuccess => this["UpdateSuccess"];

		// StringLocalizer re-publishing
		// TODO create StrongApiLocalizerBase base-class?!
		public LocalizedString this[string name] => stringLocalizer[name];
		public LocalizedString this[string name, params object[] arguments] => stringLocalizer[name, arguments];
		public string GetString(string name) => stringLocalizer.GetString(name);
		public string GetString(string name, params object[] arguments) => stringLocalizer.GetString(name, arguments);
		public IEnumerable<LocalizedString> GetAllStrings() => stringLocalizer.GetAllStrings();
		public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures = false) => stringLocalizer.GetAllStrings(includeParentCultures);

	}
}
