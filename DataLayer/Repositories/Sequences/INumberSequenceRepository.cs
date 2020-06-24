using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.Repositories;
using Havit.GoranG3.Model.Sequences;

namespace Havit.GoranG3.DataLayer.Repositories.Sequences
{
	public partial interface INumberSequenceRepository
	{
		public List<NumberSequence> GetAllIncludingDeleted();
	}
}