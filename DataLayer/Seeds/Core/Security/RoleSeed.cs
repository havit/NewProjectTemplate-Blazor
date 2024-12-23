using Havit.Data.Patterns.DataSeeds;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Security;

namespace Havit.NewProjectTemplate.DataLayer.Seeds.Core.Security;

public class RoleSeed : DataSeed<CoreProfile>
{
	public override async Task SeedDataAsync(CancellationToken cancellationToken)
	{
		var roles = Enum.GetValues<RoleEntry>().Select(entry => new Role { Id = (int)entry, Name = entry.ToString() }).ToArray();

		await SeedAsync(For(roles).PairBy(r => r.Id), cancellationToken);
	}
}
