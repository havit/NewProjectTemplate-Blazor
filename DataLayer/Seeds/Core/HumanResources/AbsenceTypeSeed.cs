using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.DataLayer.DataSources.HumanResources;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.Model.HumanResources;

namespace Havit.GoranG3.DataLayer.Seeds.Core.HumanResources
{
	public class AbsenceTypeSeed : DataSeed<CoreProfile>
	{
		private readonly IAbsenceTypeDataSource absenceTypeDataSource;

		public AbsenceTypeSeed(IAbsenceTypeDataSource absenceTypeDataSource)
		{
			this.absenceTypeDataSource = absenceTypeDataSource;
		}

		public override void SeedData()
		{
			if (absenceTypeDataSource.DataIncludingDeleted.Any())
			{
				return; // one-off seed of defaults
			}

			var absenceTypes = new[]
			{
				new AbsenceType()
				{
					MigrationId = -1,
					Name = "Dovolená",
					HasBalance = true,
					IsActive = true,
					UiOrder = 1
				},
				new AbsenceType()
				{
					MigrationId = -2,
					Name = "Náhradní volno",
					HasBalance = false,
					IsActive = false,
					UiOrder = 2
				},
				new AbsenceType()
				{
					MigrationId = -3,
					Name = "Neplacené volno",
					HasBalance = false,
					IsActive = true,
					UiOrder = 3
				},
				new AbsenceType()
				{
					MigrationId = null,
					Name = "Nemoc",
					HasBalance = false,
					IsActive = true,
					UiOrder = 4
				}
			};

			Seed(For(absenceTypes).PairBy(at => at.MigrationId).WithoutUpdate());
		}
	}
}
