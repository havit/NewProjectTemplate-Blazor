using Havit.Data.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Havit.NewProjectTemplate.Services.HealthChecks;

public class NewProjectTemplateDbContextHealthCheck : BaseHealthCheck
{
	private readonly IDbContext dbContext;

	public NewProjectTemplateDbContextHealthCheck(IDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	protected async override Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken)
	{
		return await dbContext.Database.CanConnectAsync(cancellationToken)
			? HealthCheckResult.Healthy()
			: HealthCheckResult.Unhealthy();
	}
}
