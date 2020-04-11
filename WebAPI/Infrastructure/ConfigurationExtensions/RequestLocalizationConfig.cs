using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
	public static class RequestLocalizationConfig
	{
		public static void AddCustomizedRequestLocalization(this IServiceCollection services)
		{
			// TODO: Jaká culture?
			CultureInfo czechCulture = new CultureInfo("cs-CZ");
			services.Configure<RequestLocalizationOptions>(options =>
			{
				options.DefaultRequestCulture = new RequestCulture(czechCulture, czechCulture);
				options.SupportedCultures = new List<CultureInfo> { czechCulture };
				options.SupportedUICultures = new List<CultureInfo> { czechCulture };
				options.RequestCultureProviders.Clear();
			});
		}
	}
}
