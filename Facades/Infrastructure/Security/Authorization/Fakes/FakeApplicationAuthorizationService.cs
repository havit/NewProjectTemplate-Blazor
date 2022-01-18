using System.Security.Claims;
using Havit.Data.Patterns.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization.Fakes;

/// <summary>
/// Implementace IApplicationAuthorizationService pro účely testů. Veškeré testy na oprávnění procházejí.
/// </summary>
[Fake]
public class FakeApplicationAuthorizationService : IApplicationAuthorizationService
{
	public async Task VerifyAuthorizationAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
	{
		// NOOP
		await Task.FromResult(true);
	}

	public async Task VerifyCurrentUserAuthorizationAsync(IAuthorizationRequirement requirement, object resource = null)
	{
		// NOOP
		await Task.FromResult(true);
	}

	public async Task<bool> IsAuthorizedAsync(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
	{
		return await Task.FromResult(true);
	}

	public async Task<bool> IsCurrentUserAuthorizedAsync(IAuthorizationRequirement requirement, object resource = null)
	{
		return await Task.FromResult(true);
	}
}
