using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Contracts.Crm;
using Havit.NewProjectTemplate.Web.Client.Services.DataStores;
using Microsoft.AspNetCore.Components;

namespace Havit.NewProjectTemplate.Web.Client.Pages.Development
{
	public partial class Pickers
	{
		[Inject] protected IContactReferenceDataStore ContactReferenceDataStore { get; set; }

		private IEnumerable<ContactReferenceVM> contactReferences;
		private TestModel model = new TestModel();

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			contactReferences = await ContactReferenceDataStore.GetAllAsync();
		}

		public class TestModel
		{
			public int? ContactId { get; set; }
		}
	}
}
