using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Havit.NewProjectTemplate.Services.HealthChecks;

public abstract class BaseHealthCheck : IHealthCheck
{
	async Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
	{
		try
		{
			CancellationToken token = (Timeout > 0)
				? CancellationTokenSource.CreateLinkedTokenSource(new CancellationTokenSource(Timeout).Token, cancellationToken).Token
				: cancellationToken;
			return await this.CheckHealthAsync(token);
		}
		catch (Exception exception)
		{
			return HealthCheckResult.Unhealthy(exception: exception);
		}
	}

	protected virtual int Timeout => 10_000;

	protected abstract Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken);
}
