using Havit.NewProjectTemplate.Model.Common;
using Havit.Data.Patterns.DataSeeds;

namespace Havit.NewProjectTemplate.DataLayer.Seeds.Core.Common;

public class ApplicationSettingsSeed : DataSeed<CoreProfile>
{
	public override async Task SeedDataAsync(CancellationToken cancellationToken)
	{
		ApplicationSettings settings = new ApplicationSettings
		{
			Id = (int)ApplicationSettings.Entry.Current,
			// TODO: Výchozí nastavení
		};

		await SeedAsync(For(settings).PairBy(item => item.Id).WithoutUpdate(), cancellationToken);
	}
}
