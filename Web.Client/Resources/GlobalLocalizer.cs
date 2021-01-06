using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Havit.GoranG3.Web.Client.Resources
{
	public class GlobalLocalizer : IStringLocalizer<GlobalLocalizer>, IGlobalLocalizer
	{
		private readonly IStringLocalizer<GlobalLocalizer> stringLocalizer;

		public GlobalLocalizer(IStringLocalizer<GlobalLocalizer> stringLocalizer)
		{
			this.stringLocalizer = stringLocalizer;
		}

		// string-API properties
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
