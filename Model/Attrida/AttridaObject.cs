using Havit.Model.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Attrida
{
	public class AttridaObject
	{
		public int Id { get; set; }

		public List<AttridaTag> Tags { get; set; }

		public List<AttridaDocument> DocumentsIncludingDeleted { get; } = new List<AttridaDocument>();
		public FilteringCollection<AttridaDocument> Documents { get; }

		public List<AttridaComment> CommentsIncludingDeleted { get; } = new List<AttridaComment>();
		public FilteringCollection<AttridaComment> Comments { get; }

		public AttridaObject()
		{
			this.Documents = new FilteringCollection<AttridaDocument>(this.DocumentsIncludingDeleted, d => d.Deleted is null);
			this.Comments = new FilteringCollection<AttridaComment>(this.CommentsIncludingDeleted, d => d.Deleted is null);
		}
	}
}
