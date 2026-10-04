using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Model.Security;
using Havit.NewProjectTemplate.Primitives.Security;

namespace Havit.NewProjectTemplate.Services.Infrastructure.Security;

[Service(Profile = ServiceProfiles.WebServer)]
public class ApplicationAuthorizationService : IApplicationAuthorizationService
{
	private readonly IApplicationAuthenticationService _applicationAuthenticationService;

	public ApplicationAuthorizationService(IApplicationAuthenticationService applicationAuthenticationService)
	{
		_applicationAuthenticationService = applicationAuthenticationService;
	}

	public IEnumerable<RoleEntry> GetCurrentUserRoles()
	{
		var roles = new List<RoleEntry>();
		foreach (var identity in _applicationAuthenticationService.GetCurrentClaimsPrincipal().Identities)
		{
			roles.AddRange(identity.FindAll(identity.RoleClaimType).Select(c => Enum.Parse<RoleEntry>(c.Value)));
		}
		return roles;
	}

	public bool IsCurrentUserInRole(RoleEntry role)
	{
		return _applicationAuthenticationService.GetCurrentClaimsPrincipal().IsInRole(role.ToString());
	}
}
