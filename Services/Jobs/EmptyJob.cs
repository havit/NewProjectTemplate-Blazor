using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Havit.NewProjectTemplate.Services.Jobs;

[Service(Profile = ServiceProfiles.JobsRunner)]
public class EmptyJob : IEmptyJob
{
	private readonly ILogger<EmptyJob> _logger;

	public EmptyJob(ILogger<EmptyJob> logger)
	{
		_logger = logger;
	}

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Begin: EmptyJob");

		// TODO - Replace job code here.
		await Task.Delay(1, cancellationToken);

		_logger.LogInformation("End: EmptyJob");
	}
}
