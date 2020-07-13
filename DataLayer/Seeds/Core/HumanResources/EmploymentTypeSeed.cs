using Havit.Data.Patterns.DataSeeds;
using Havit.GoranG3.DataLayer.Repositories.HumanResources;
using Havit.GoranG3.Model.HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.DataLayer.Seeds.Core.HumanResources
{
	public class EmploymentTypeSeed : DataSeed<CoreProfile>
	{
		private readonly IEmploymentTypeRepository employmentTypeRepository;

		public EmploymentTypeSeed(IEmploymentTypeRepository employmentTypeRepository)
		{
			this.employmentTypeRepository = employmentTypeRepository;
		}

		public override void SeedData()
		{
			if (employmentTypeRepository.GetAll().Any())
			{
				return; // one-off seed of defaults
			}

			var employmentTypes = new[]
			{
				new EmploymentType()
				{
					Name = "Hlavní pracovní poměr",
					EmployerContributionsRate = 0.350m
				},
				new EmploymentType()
				{
					Name = "Dohoda o provedení práce",
					EmployerContributionsRate = 0.000m
				},
				new EmploymentType()
				{
					Name = "Ičař - živnostník",
					EmployerContributionsRate = 0.000m
				},
				new EmploymentType()
				{
					Name = "Dohoda o pracovní činnosti",
					EmployerContributionsRate = 0.350m
				}
			};

			Seed(For(employmentTypes).PairBy(at => at.Name).WithoutUpdate());
		}
	}
}
