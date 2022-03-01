using System.Security;
using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authentication;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization;

[Service(Profile = ServiceProfiles.WebServer)]
public class ApplicationAuthorizationService : IApplicationAuthorizationService
{
	private readonly IApplicationAuthenticationService applicationAuthenticationService;
	private readonly IAuthorizationService authorizationService;

	public ApplicationAuthorizationService(IApplicationAuthenticationService applicationAuthenticationService, IAuthorizationService authorizationService)
	{
		this.applicationAuthenticationService = applicationAuthenticationService;
		this.authorizationService = authorizationService;
	}

	public async Task<bool> IsAuthorizedAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentNullException>(requirement != null);

		return (await authorizationService.AuthorizeAsync(user, resource, requirement)).Succeeded;
	}

	public async Task VerifyAuthorizationAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
	{
		Contract.Requires<ArgumentNullException>(user != null);
		Contract.Requires<ArgumentNullException>(requirement != null);

		if (!await IsAuthorizedAsync(user, requirement, resource))
		{
			throw new SecurityException();
		}
	}

	public async Task<bool> IsCurrentUserAuthorizedAsync(IAuthorizationRequirement requirement, object resource = null)
	{
		Contract.Requires<ArgumentNullException>(requirement != null);

		return await IsAuthorizedAsync(applicationAuthenticationService.GetCurrentClaimsPrincipal(), requirement, resource);
	}

	public async Task VerifyCurrentUserAuthorizationAsync(IAuthorizationRequirement requirement, object resource = null)
	{
		Contract.Requires<ArgumentNullException>(requirement != null);

		await VerifyAuthorizationAsync(applicationAuthenticationService.GetCurrentClaimsPrincipal(), requirement, resource);
	}
}
