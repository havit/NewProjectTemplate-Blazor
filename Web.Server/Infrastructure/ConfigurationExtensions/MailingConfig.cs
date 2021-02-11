using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.GoranG3.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.GoranG3.Web.Server.Infrastructure.ConfigurationExtensions
{
	public static class MailingConfig
	{
		public static void AddCustomizedMailing(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MailingOptions>(configuration.GetSection("AppSettings:MailingOptions"));
		}
	}
}
