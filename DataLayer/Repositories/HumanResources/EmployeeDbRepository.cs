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
using Havit.GoranG3.Model.HumanResources;

namespace Havit.GoranG3.DataLayer.Repositories.HumanResources
{
	public partial class EmployeeDbRepository : IEmployeeRepository
	{
		public List<Employee> GetAllIncludingDeleted()
		{
			return DataIncludingDeleted.Include(GetLoadReferences).ToList();
		}

		protected override IEnumerable<Expression<Func<Employee, object>>> GetLoadReferences()
		{
			yield return (Employee employee) => employee.PrivateTeam;
			yield return (Employee employee) => employee.Contact;
			yield return (Employee employee) => employee.User;
		}
	}
}