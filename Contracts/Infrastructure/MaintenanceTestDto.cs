using FluentValidation;

namespace Havit.NewProjectTemplate.Contracts.Infrastructure;

public record MaintenanceTestDto
{
	public string Value { get; set; }

	public class MaintenanceTestDtoValidator : AbstractValidator<MaintenanceTestDto>
	{
		public MaintenanceTestDtoValidator()
		{
			RuleFor(x => x.Value).NotEmpty();
		}
	}
}
