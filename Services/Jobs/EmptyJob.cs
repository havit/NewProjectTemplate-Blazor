using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.Services.Jobs;

[Service(Profile = ServiceProfiles.Utility)]
public class EmptyJob : IEmptyJob
{
	private readonly ILogger<EmptyJob> logger;

	public EmptyJob(ILogger<EmptyJob> logger)
	{
		this.logger = logger;
	}

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("Begin: EmptyJob");

		// TODO - Replace job code here.
		await Task.Delay(1, cancellationToken);

		logger.LogInformation("End: EmptyJob");
	}
}
