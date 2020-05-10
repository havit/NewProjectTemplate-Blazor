using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Pages.Prototyping
{
    public partial class HxBoundFieldGridColumn : HxGridColumnBase
	{
		[Parameter]
		public string FieldName { get; set; }

		[Parameter]
		public string DataFormatString { get; set; }
	}
}
