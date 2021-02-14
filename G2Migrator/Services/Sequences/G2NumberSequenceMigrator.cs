using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.Sequences;
using Havit.NewProjectTemplate.Model.Sequences;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.Sequences
{
	[Service]
	public class G2NumberSequenceMigrator : IG2NumberSequenceMigrator
	{
		private readonly MigrationOptions options;
		private readonly INumberSequenceRepository numberSequenceRepository;
		private readonly IUnitOfWork unitOfWork;

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
			using SqlCommand cmd = new SqlCommand("WITH data AS (SELECT c.*, CASE WHEN EXISTS(SELECT * FROM dbo.CiselnaRada_CiselnaRadaTarget t WHERE t.CiselnaRadaID = c.CiselnaRadaID AND t.CiselnaRadaTargetID = -1 /* InvoiceIssued */) THEN 1 ELSE 0 END AS HasInvoiceIssuedTarget, CASE WHEN EXISTS(SELECT * FROM dbo.CiselnaRada_CiselnaRadaTarget t WHERE t.CiselnaRadaID = c.CiselnaRadaID AND t.CiselnaRadaTargetID = -2 /* InvoiceRecieved */) THEN 1 ELSE 0 END AS HasInvoiceReceivedTarget FROM dbo.CiselnaRada c) SELECT * FROM data WHERE HasInvoiceIssuedTarget = 1 OR HasInvoiceReceivedTarget = 1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var sequences = numberSequenceRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var sequenceID = reader.GetValue<int>("CiselnaRadaID");
				Console.Write("Number sequence: " + sequenceID);
				var sequence = sequences.Find(s => s.MigrationId == sequenceID);

				if (sequence == null)
				{
					sequence = new NumberSequence();
					sequence.MigrationId = sequenceID;
					unitOfWork.AddForInsert(sequence);
					Console.WriteLine(" INSERT");
				}
				else
				{
					unitOfWork.AddForUpdate(sequence);
					Console.WriteLine(" UPDATE");
				}

				var hasInvoiceReceivedTarget = (reader.GetValue<int>("HasInvoiceReceivedTarget") == 1);
				var hasInvoiceIssuedTarget = (reader.GetValue<int>("HasInvoiceIssuedTarget") == 1);

				sequence.Targets = NumberSequenceTarget.None;

				if (hasInvoiceIssuedTarget)
				{
					sequence.Targets |= NumberSequenceTarget.InvoiceIssued;
				}
				if (hasInvoiceReceivedTarget)
				{
					sequence.Targets |= NumberSequenceTarget.InvoiceReceived;
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