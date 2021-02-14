using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.Model.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.DataLayer.Seeds.Core.Localizations
{
	public class LanguageSeed : DataSeed<CoreProfile>
	{
		public override void SeedData()
		{
			var languages = new[]
			{
				new Language()
				{
					Id = (int)Language.Entry.Czech,
					Name = "Čeština",
					Culture = String.Empty,
					UiCulture = "cs-CZ"
				},
				new Language()
				{
					Id = (int)Language.Entry.English,
					Name = "English",
					Culture = "en",
					UiCulture = "en-US"
				}
			};

			Seed(For(languages).PairBy(language => language.Id));
		}
	}
}
