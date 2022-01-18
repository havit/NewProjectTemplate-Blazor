using Havit.NewProjectTemplate.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ConfigurationExtensions;

public static class MailingConfig
{
	public static void AddCustomizedMailing(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<MailingOptions>(configuration.GetSection("AppSettings:MailingOptions"));
	}
}
