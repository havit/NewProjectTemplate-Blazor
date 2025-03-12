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

internal class ApplicationSettingsDbRepositoryQueryProvider : IRepositoryQueryProvider<Havit.NewProjectTemplate.Model.Common.ApplicationSettings, System.Int32>
{
	private readonly ISoftDeleteManager _softDeleteManager;

	private readonly Func<DbContext, System.Int32, Havit.NewProjectTemplate.Model.Common.ApplicationSettings> _getObjectQuery;
	private readonly Func<DbContext, System.Int32, CancellationToken, Task<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> _getObjectAsyncQuery;
	private readonly Func<DbContext, System.Int32[], IEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> _getObjectsQuery;
	private readonly Func<DbContext, System.Int32[], IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> _getObjectsAsyncQuery;
	private readonly Func<DbContext, IEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> _getAllQuery;
	private readonly Func<DbContext, IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> _getAllAsyncQuery;

	public ApplicationSettingsDbRepositoryQueryProvider(ISoftDeleteManager softDeleteManager)
	{
		_softDeleteManager = softDeleteManager;

		_getObjectQuery = EF.CompileQuery((DbContext dbContext, System.Int32 id) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetObject")
			.Where(entity => entity.Id == id)
			.FirstOrDefault());

		_getObjectAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext, System.Int32 id, CancellationToken cancellationToken) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetObjectAsync")
			.Where(entity => entity.Id == id)
			.FirstOrDefault());

		_getObjectsQuery = EF.CompileQuery((DbContext dbContext, System.Int32[] ids) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetObjects")
			.Where(entity => ids.Contains(entity.Id)));

		_getObjectsAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext, System.Int32[] ids) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetObjectsAsync")
			.Where(entity => ids.Contains(entity.Id)));

		_getAllQuery = EF.CompileQuery((DbContext dbContext) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetAll")
			.WhereNotDeleted(_softDeleteManager));

		_getAllAsyncQuery = EF.CompileAsyncQuery((DbContext dbContext) => dbContext
			.Set<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>()
			.TagWith("ApplicationSettingsDbRepository.GetAllAsync")
			.WhereNotDeleted(_softDeleteManager));
	}

	public Func<DbContext, System.Int32, Havit.NewProjectTemplate.Model.Common.ApplicationSettings> GetGetObjectQuery() => _getObjectQuery;
	public Func<DbContext, System.Int32, CancellationToken, Task<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> GetGetObjectAsyncQuery() => _getObjectAsyncQuery;
	public Func<DbContext, System.Int32[], IEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> GetGetObjectsQuery() => _getObjectsQuery;
	public Func<DbContext, System.Int32[], IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> GetGetObjectsAsyncQuery() => _getObjectsAsyncQuery;
	public Func<DbContext, IAsyncEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> GetGetAllAsyncQuery() => _getAllAsyncQuery;
	public Func<DbContext, IEnumerable<Havit.NewProjectTemplate.Model.Common.ApplicationSettings>> GetGetAllQuery() => _getAllQuery;
}
