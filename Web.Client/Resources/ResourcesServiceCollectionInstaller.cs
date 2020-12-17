using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Web.Client.Resources
{
	public static class ResourcesServiceCollectionInstaller
	{
		/// <summary>
		/// Adds generated strong-API wrappers for localization resources.
		/// </summary>
		public static void AddGeneratedResourceWrappers(this IServiceCollection services)
		{
			services.AddScoped<IBankAccountLocalizer, BankAccountLocalizer>();
			// ...
		}
	}
}
