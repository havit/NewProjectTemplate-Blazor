using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.DataLayer.Repositories.HumanResources;
using Havit.NewProjectTemplate.Model.HumanResources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.G2Migrator.Services.HumanResources
{
	[Service]
	public class G2AbsenceTypeMigrator : IG2AbsenceTypeMigrator
	{
		private readonly MigrationOptions options;
		private readonly IAbsenceTypeRepository absenceTypeRepository;
		private readonly IUnitOfWork unitOfWork;

		public G2AbsenceTypeMigrator(
			IOptions<MigrationOptions> options,
			IAbsenceTypeRepository absenceTypeRepository,
			IUnitOfWork unitOfWork)
		{
			this.options = options.Value;
			this.absenceTypeRepository = absenceTypeRepository;
			this.unitOfWork = unitOfWork;
		}
		public void MigrateAbsenceTypes()
		{
			using SqlConnection conn = new SqlConnection(options.G2ConnectionString);
			conn.Open();
			using SqlCommand cmd = new SqlCommand("SELECT types.*, loc.Nazev FROM TypPracovnihoVolna types JOIN TypPracovnihoVolnaLocalization loc ON loc.TypPracovnihoVolnaID = types.TypPracovnihoVolnaID WHERE LanguageID = 1", conn);
			using SqlDataReader reader = cmd.ExecuteReader();

			var absenceTypes = absenceTypeRepository.GetAllIncludingDeleted();

			while (reader.Read())
			{
				var absenceTypeID = reader.GetValue<int>("TypPracovnihoVolnaID");
				Console.Write("TypPracovnihoVolna => AbsenceType: " + absenceTypeID);
				var absenceType = absenceTypes.Find(a => a.MigrationId == absenceTypeID);
				var name = reader.GetValue<string>("Nazev");

				if (name.Equals("nemoc", StringComparison.InvariantCultureIgnoreCase))
				{
					var nemocType = absenceTypes.Find(n => n.Name == "Nemoc");
					nemocType.MigrationId = absenceTypeID;
					unitOfWork.AddForUpdate(nemocType);
					Console.WriteLine(" UPDATE");
				}
				else if (absenceTypeID > 0)
				{
					if (absenceType == null)
					{
						absenceType = new AbsenceType();
						absenceType.MigrationId = absenceTypeID;
						unitOfWork.AddForInsert(absenceType);
						Console.WriteLine(" INSERT");
					}
					else
					{
						unitOfWork.AddForUpdate(absenceType);
						Console.WriteLine(" UPDATE");
					}

					absenceType.Name = name;
					absenceType.HasBalance = reader.GetValue<bool>("SnizujeFondPracovniDoby");

					var uiOrder = (int)reader.GetValue<decimal>("Poradi");
					absenceType.UiOrder = uiOrder + 4; //The first 4 values are seeded.
				}
			}

			unitOfWork.Commit();
		}
	}
}
