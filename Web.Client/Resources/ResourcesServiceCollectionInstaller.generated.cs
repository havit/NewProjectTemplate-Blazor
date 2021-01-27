using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.GoranG3.Web.Client.Resources.Model.Finance;
using Havit.GoranG3.Web.Client.Resources.Pages.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Web.Client.Resources
{
	public static partial class ResourcesServiceCollectionInstaller
	{
		/// <summary>
		/// Adds generated strong-API wrappers for localization resources.
		/// </summary>
		public static void AddGeneratedResourceWrappers(this IServiceCollection services)
		{
			services.AddScoped<IGlobalLocalizer, GlobalLocalizer>();
			services.AddScoped<INavigationLocalizer, NavigationLocalizer>();


			services.AddScoped<IBankAccountLocalizer, BankAccountLocalizer>();
			services.AddScoped<ICurrencyLocalizer, CurrencyLocalizer>();
			services.AddScoped<IExchangeRateLocalizer, ExchangeRateLocalizer>();

			services.AddScoped<IAdminIndexLocalizer, AdminIndexLocalizer>();
		}
	}
}
