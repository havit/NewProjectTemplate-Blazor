using Havit.Services.FileStorage;

namespace Havit.NewProjectTemplate.Services.Infrastructure.FileStorages;

public class ApplicationFileStorageService : FileStorageWrappingService<ApplicationFileStorage>, IApplicationFileStorageService
{
	public ApplicationFileStorageService(IFileStorageService<ApplicationFileStorage> fileStorageService) : base(fileStorageService)
	{
	}
}
