using Havit.Data.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Havit.NewProjectTemplate.Services.HealthChecks;

public class NewProjectTemplateDbContextHealthCheck : BaseHealthCheck
{
	private readonly IDbContext _dbContext;

	public NewProjectTemplateDbContextHealthCheck(IDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	protected async override Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken)
	{
		return await _dbContext.Database.CanConnectAsync(cancellationToken)
			? HealthCheckResult.Healthy()
			: HealthCheckResult.Unhealthy();
	}
}
