using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.DataLayer.Repositories.Sequences;
using Havit.GoranG3.Model.Sequences;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.GoranG3.G2Migrator.Services.Sequences
{
	[Service]
	public class G2NumberSequenceUnusedNumberMigrator : IG2NumberSequenceUnusedNumberMigrator
	{
		private readonly MigrationOptions options;
		private readonly INumberSequenceUnusedNumberRepository numberSequenceUnusedNumberRepository;
		private readonly INumberSequenceRepository numberSequenceRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2NumberSequenceUnusedNumberMigrator(
			IOptions<MigrationOptions> options,
			INumberSequenceUnusedNumberRepository numberSequenceUnusedNumberRepository,
			INumberSequenceRepository numberSequenceRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.numberSequenceUnusedNumberRepository = numberSequenceUnusedNumberRepository;
			this.numberSequenceRepository = numberSequenceRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateUnusedNumbers()
		{
			var unusedNumbers = numberSequenceUnusedNumberRepository.GetAll();

			if (!unusedNumbers.Any())
			{
				using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
				conn.Open();
				using SqlCommand cmd = new SqlCommand("SELECT * FROM CiselnaRadaVolneCislo", conn);
				using SqlDataReader reader = cmd.ExecuteReader();

				var numberSequences = numberSequenceRepository.GetAllIncludingDeleted();

				while (reader.Read())
				{
					var unusedNumberSequence = numberSequences.Find(s => s.MigrationId == reader.GetValue<int>("CiselnaRadaID"));

					if (unusedNumberSequence != null)
					{
						var unusedNumberID = reader.GetValue<int>("CiselnaRadaVolneCisloID");
						var unusedNumberValue = reader.GetValue<int>("Hodnota");

						Console.Write("Unused number: " + unusedNumberID);
						var unusedNumber = unusedNumbers.Find(n => n.NumberSequence == unusedNumberSequence && n.Value == unusedNumberValue);

						if (unusedNumber == null)
						{
							unusedNumber = new NumberSequenceUnusedNumber();
							unitOfWork.AddForInsert(unusedNumber);
							Console.WriteLine(" INSERT");
						}
						else // Maybe useful in future
						{
							unitOfWork.AddForUpdate(unusedNumber);
							Console.WriteLine(" UPDATE");
						}

						unusedNumber.NumberSequence = unusedNumberSequence;
						unusedNumber.Value = unusedNumberValue;
					}
				}

				unitOfWork.Commit();
			}
		}
	}
}
