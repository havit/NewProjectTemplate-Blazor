using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources.Model.Finance
{
	public class CurrencyLocalizer : ICurrencyLocalizer, IStringLocalizer<CurrencyLocalizer>
	{
		private readonly IStringLocalizer<CurrencyLocalizer> stringLocalizer;

		public CurrencyLocalizer(IStringLocalizer<CurrencyLocalizer> stringLocalizer)
		{
			this.stringLocalizer = stringLocalizer;
		}

		// string-API properties
		public LocalizedString Code => this["Code"];
		public LocalizedString DefaultBankAccount => this["DefaultBankAccount"];
		public LocalizedString Delete => this["Delete"];
		public LocalizedString DeleteConfirmation => this["DeleteConfirmation"];
		public LocalizedString New => this["New"];
		public LocalizedString Plural => this["Plural"];
		public LocalizedString Singular => this["Singular"];

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
