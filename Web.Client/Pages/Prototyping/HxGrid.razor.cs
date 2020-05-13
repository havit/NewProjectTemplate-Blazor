using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class HxGrid<T> : ComponentBase
    {
		[Parameter]
		public IEnumerable<T> Data { get; set; }

		[Parameter]
		public bool AllowSelection { get; set; }

		[Parameter]
		public bool AllowSorting { get; set; }

		[Parameter]
		public RenderFragment<T> Columns { get; set; }

		[Parameter]
		public RenderFragment<T> ContextMenu { get; set; }

		[Parameter]
		public T SelectedDataItem { get; set; }

		[Parameter]
		public EventCallback<T> SelectedDataItemChanged { get; set; }

	}
}
