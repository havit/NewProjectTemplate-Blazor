using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.Model.Localizations;

namespace Havit.NewProjectTemplate.DataLayer.Seeds.Core.Localizations;

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
					Culture = "cs-CZ",
					UiCulture = String.Empty
				},
				new Language()
				{
					Id = (int)Language.Entry.English,
					Name = "English",
					Culture = "en-US",
					UiCulture = "en"
				}
			};

		Seed(For(languages).PairBy(language => language.Id));
	}
}
