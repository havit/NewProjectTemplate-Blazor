using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Havit.NewProjectTemplate.Contracts.Crm
{
	public record ContactReferenceVM
	{
		public int Id { get; init; }

		public string Name { get; init; }

		public bool IsDeleted { get; init; }
	}
}
