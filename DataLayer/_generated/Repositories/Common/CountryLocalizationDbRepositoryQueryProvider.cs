﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Microsoft.EntityFrameworkCore;
using DbContext = Havit.Data.EntityFrameworkCore.DbContext;

namespace Havit.NewProjectTemplate.DataLayer.Repositories.Common;

internal class CountryLocalizationDbRepositoryQueryProvider : IRepositoryQueryProvider<Havit.NewProjectTemplate.Model.Common.CountryLocalization, System.Int32>
{
	private readonly ISoftDeleteManager _softDeleteManager;

	private readonly Func<DbContext, System.Int32, Havit.NewProjectTemplate.Model.Common.CountryLocalization> _getObjectQuery;
	private readonly Func<DbContext, System.Int32, CancellationToken, Task<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> _getObjectAsyncQuery;
	private readonly Func<DbContext, System.Int32[], IEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> _getObjectsQuery;
	private readonly Func<DbContext, System.Int32[], IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> _getObjectsAsyncQuery;
	private readonly Func<DbContext, IEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> _getAllQuery;
	private readonly Func<DbContext, IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> _getAllAsyncQuery;

	public CountryLocalizationDbRepositoryQueryProvider(ISoftDeleteManager softDeleteManager)
	{
		_softDeleteManager = softDeleteManager;

		_getObjectQuery = EF.CompileQuery((DbContext dbContext, System.Int32 id) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetObject")
			.Where(entity => entity.Id == id)
			.FirstOrDefault());

		_getObjectAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext, System.Int32 id, CancellationToken cancellationToken) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetObjectAsync")
			.Where(entity => entity.Id == id)
			.FirstOrDefault());

		_getObjectsQuery = EF.CompileQuery((DbContext dbContext, System.Int32[] ids) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetObjects")
			.Where(entity => ids.Contains(entity.Id)));

		_getObjectsAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext, System.Int32[] ids) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetObjectsAsync")
			.Where(entity => ids.Contains(entity.Id)));

		_getAllQuery = EF.CompileQuery((DbContext dbContext) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetAll")
			.WhereNotDeleted(_softDeleteManager));

		_getAllAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.CountryLocalization>()
			.TagWith("CountryLocalizationDbRepository.GetAllAsync")
			.WhereNotDeleted(_softDeleteManager));
	}

	public Func<DbContext, System.Int32, Havit.NewProjectTemplate.Model.Common.CountryLocalization> GetGetObjectQuery() => _getObjectQuery;
	public Func<DbContext, System.Int32, CancellationToken, Task<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> GetGetObjectAsyncQuery() => _getObjectAsyncQuery;
	public Func<DbContext, System.Int32[], IEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> GetGetObjectsQuery() => _getObjectsQuery;
	public Func<DbContext, System.Int32[], IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> GetGetObjectsAsyncQuery() => _getObjectsAsyncQuery;
	public Func<DbContext, IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> GetGetAllAsyncQuery() => _getAllAsyncQuery;
	public Func<DbContext, IEnumerable<Havit.NewProjectTemplate.Model.Common.CountryLocalization>> GetGetAllQuery() => _getAllQuery;
}
