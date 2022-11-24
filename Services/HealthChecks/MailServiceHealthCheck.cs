using Havit.NewProjectTemplate.Services.Mailing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Havit.NewProjectTemplate.Services.HealthChecks;

public class MailServiceHealthCheck : BaseHealthCheck
{
	private readonly IMailingService mailingService;

	public MailServiceHealthCheck(IMailingService mailingService)
	{
		this.mailingService = mailingService;
	}

	protected async override Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken)
	{
		await mailingService.VerifyHealthAsync(cancellationToken);
		return HealthCheckResult.Healthy();
	}
}
