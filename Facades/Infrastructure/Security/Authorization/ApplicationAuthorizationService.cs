using System.Security;
using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;
using Havit.NewProjectTemplate.Primitives.Model.Security;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

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
		return _applicationAuthenticationService.GetCurrentClaimsPrincipal().FindAll(ClaimTypes.Role).Select(c => Enum.Parse<RoleEntry>(c.Value));
	}

	public bool IsCurrentUserInRole(RoleEntry role)
	{
		return _applicationAuthenticationService.GetCurrentClaimsPrincipal().IsInRole(role.ToString());
	}
}
