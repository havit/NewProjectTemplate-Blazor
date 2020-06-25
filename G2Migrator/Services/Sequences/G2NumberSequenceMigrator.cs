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
	public class G2NumberSequenceMigrator : IG2NumberSequenceMigrator
	{
		private readonly MigrationOptions options;
		private readonly INumberSequenceRepository numberSequenceRepository;
		private readonly IUnitOfWork unitOfWork;
		private NumberSequenceTarget numberSequenceTarget;

		public G2NumberSequenceMigrator(
			IOptions<MigrationOptions> options,
			INumberSequenceRepository numberSequenceRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.numberSequenceRepository = numberSequenceRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateSequences()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT cis.*, tar.PropertyName FROM CiselnaRada cis FULL OUTER JOIN CiselnaRada_CiselnaRadaTarget rel ON cis.CiselnaRadaID = rel.CiselnaRadaID FULL OUTER JOIN CiselnaRadaTarget tar ON rel.CiselnaRadaTargetID = tar.CiselnaRadaTargetID WHERE tar.PropertyName = 'FakturaPrijata' OR tar.PropertyName = 'FakturaVystavena'", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var sequences = numberSequenceRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var sequenceID = reader.GetValue<int>("CiselnaRadaID");
				Console.Write("Number sequence: " + sequenceID);
				string target = reader.GetValue<string>("PropertyName");

				switch (target)
				{
					case "FakturaPrijata":
						numberSequenceTarget = NumberSequenceTarget.InvoiceReceived;
						break;

					case "FakturaVystavena":
						numberSequenceTarget = NumberSequenceTarget.InvoiceIssued;
						break;

					default:
						Console.WriteLine("Incorrect NumberSequenceTarget");
						break;
				}

				var sequence = sequences.Find(s => s.MigrationId == sequenceID);// && s.Targets.HasFlag(numberSequenceTarget));
				if (sequence == null)
				{
					sequence = new NumberSequence();
					sequence.MigrationId = sequenceID;
					unitOfWork.AddForInsert(sequence);
					Console.WriteLine(" INSERT");
					sequence.Targets = numberSequenceTarget;
				}
				else
				{
					unitOfWork.AddForUpdate(sequence);
					Console.WriteLine(" UPDATE");
					if (!sequence.Targets.HasFlag(numberSequenceTarget))
					{
						sequence.Targets = NumberSequenceTarget.All;
					}
				}

				sequence.Name = reader.GetValue<string>("Nazev");
				sequence.Prefix = reader.GetValue<string>("Prefix");
				sequence.Suffix = reader.GetValue<string>("Suffix");
				sequence.DigitCount = reader.GetValue<int?>("PocetCislic");
				sequence.InitialValue = reader.GetValue<int>("PocatecniHodnota");
				sequence.LastValue = reader.GetValue<int?>("PosledniPouzitaHodnota");
				sequence.IsActive = reader.GetValue<bool>("Aktivni");
				sequence.StartDate = reader.GetValue<DateTime?>("PouzitelnaOd");
				sequence.EndDate = reader.GetValue<DateTime?>("PouzitelnaDo");
				sequence.Created = reader.GetValue<DateTime>("Created");
				sequence.Deleted = reader.GetValue<DateTime?>("Deleted");
			}

			unitOfWork.Commit();
		}
	}
}