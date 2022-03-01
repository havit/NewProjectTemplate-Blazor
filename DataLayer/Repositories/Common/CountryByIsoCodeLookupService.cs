using System.Linq.Expressions;
using Havit.Data.EntityFrameworkCore.Patterns.Lookups;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataSources;
using Havit.Data.Patterns.Infrastructure;
using Havit.Data.Patterns.Repositories;
using Havit.NewProjectTemplate.Model.Common;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Common;

public class CountryByIsoCodeLookupService : LookupServiceBase<string, Country>, ICountryByIsoCodeLookupService
{
	public CountryByIsoCodeLookupService(
		IEntityLookupDataStorage lookupStorage,
		IRepository<Country> repository,
		IDataSource<Country> dataSource,
		IEntityKeyAccessor entityKeyAccessor,
		ISoftDeleteManager softDeleteManager) : base(lookupStorage, repository, dataSource, entityKeyAccessor, softDeleteManager)
	{
	}

	public Country GetCountryByIsoCode(string isoCode)
	{
		Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(isoCode));

		return GetEntityByLookupKey(isoCode.ToUpper());
	}

	protected override Expression<Func<Country, string>> LookupKeyExpression => country => country.IsoCode;
	protected override LookupServiceOptimizationHints OptimizationHints => LookupServiceOptimizationHints.EntityIsReadOnly;
}
