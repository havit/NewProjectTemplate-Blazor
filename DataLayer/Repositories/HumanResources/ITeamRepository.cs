using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.Repositories;
using Havit.GoranG3.Model.HumanResources;

namespace Havit.GoranG3.DataLayer.Repositories.HumanResources
{
	public partial interface ITeamRepository
	{
		public List<Team> GetAllIncludingDeleted();
	}
}