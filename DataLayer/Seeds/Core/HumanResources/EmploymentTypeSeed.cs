using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.DataLayer.DataSources.HumanResources;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.Model.HumanResources;
using Havit.Services.TimeServices;

namespace Havit.GoranG3.DataLayer.Seeds.Core.HumanResources
{
	public class EmploymentTypeSeed : DataSeed<CoreProfile>
	{
		private readonly IEmploymentTypeDataSource employmentTypeDataSource;
		private readonly ITimeService timeService;

		public EmploymentTypeSeed(
			IEmploymentTypeDataSource employmentTypeDataSource,
			ITimeService timeService)
		{
			this.employmentTypeDataSource = employmentTypeDataSource;
			this.timeService = timeService;
		}

		public override void SeedData()
		{
			if (employmentTypeDataSource.DataIncludingDeleted.Any())
			{
				return; // one-off seed of defaults
			}

			var employmentTypes = new[]
			{
				new EmploymentType()
				{
					Name = "Hlavní pracovní poměr",
					EmployerContributionsRate = 0.350m,
					Created = timeService.GetCurrentTime(),
				},
				new EmploymentType()
				{
					Name = "Dohoda o provedení práce",
					EmployerContributionsRate = 0.000m,
					Created = timeService.GetCurrentTime(),
				},
				new EmploymentType()
				{
					Name = "Ičař - živnostník",
					EmployerContributionsRate = 0.000m,
					Created = timeService.GetCurrentTime(),
				},
				new EmploymentType()
				{
					Name = "Dohoda o pracovní činnosti",
					EmployerContributionsRate = 0.350m,
					Created = timeService.GetCurrentTime(),
				}
			};

			Seed(For(employmentTypes).PairBy(at => at.Name).WithoutUpdate());
		}
	}
}
