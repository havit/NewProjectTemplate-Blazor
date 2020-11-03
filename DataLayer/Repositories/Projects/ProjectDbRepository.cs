using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore;
using Havit.Data.EntityFrameworkCore.Patterns.Repositories;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.DataEntries;
using Havit.Data.Patterns.DataLoaders;
using Havit.GoranG3.Model.Projects;
using Microsoft.EntityFrameworkCore;

namespace Havit.GoranG3.DataLayer.Repositories.Projects
{
	public partial class ProjectDbRepository : IProjectRepository
	{
		public List<Project> GetAllIncludingDeleted()
		{
			return DataIncludingDeleted.Include(GetLoadReferences).ToList();
		}

		protected override IEnumerable<Expression<Func<Project, object>>> GetLoadReferences()
		{
			yield return (Project p) => p.AllChildrenAndMeRelations;
			yield return (Project p) => p.AllParentsAndMeRelations;
			yield return (Project p) => p.ChildrenIncludingDeleted;
			yield return (Project p) => p.Parent;
		}
	}
}