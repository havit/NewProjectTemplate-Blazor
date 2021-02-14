using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Timesheets;
using Havit.NewProjectTemplate.Model.Timesheets;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Timesheets
{
	[Service]
	public class G2OverheadToPersonalCostsRatioMigrator : IG2OverheadToPersonalCostsRatioMigrator
	{
		private readonly MigrationOptions options;
		private readonly IOverheadToPersonalCostsRatioRepository overheadToPersonalCostsRatioRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2OverheadToPersonalCostsRatioMigrator(
			IOptions<MigrationOptions> options,
			IOverheadToPersonalCostsRatioRepository overheadToPersonalCostsRatioRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.overheadToPersonalCostsRatioRepository = overheadToPersonalCostsRatioRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateOverheadToPersonalCostsRatios()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT * FROM RezijniPrirazkaOsobnichNakladu", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var ratios = overheadToPersonalCostsRatioRepository.GetAll();

			while (reader.Read())
			{
				var startDate = reader.GetValue<DateTime>("DatumOd");
				Console.Write("RezijniPrirazkaOsobnichNakladu => OverheadToPersonalCostsRatio: " + startDate);
				var ratio = ratios.Find(p => p.StartDate == startDate);
				if (ratio == null)
				{
					ratio = new OverheadToPersonalCostsRatio();
					ratio.StartDate = startDate;
					unitOfWork.AddForInsert(ratio);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(ratio);
					Console.WriteLine(" UPDATE");
				}

				ratio.Ratio = reader.GetValue<decimal>("KoeficientPrirazky");
			}

			unitOfWork.Commit();
		}
	}
}
