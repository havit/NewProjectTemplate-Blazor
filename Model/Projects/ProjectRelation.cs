using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Projects
{
	public class ProjectRelation
	{
		public int HigherProjectId { get; set; }
		public Project HigherProject { get; set; }

		public int LowerProjectId { get; set; }
		public Project LowerProject { get; set; }
	}
}
