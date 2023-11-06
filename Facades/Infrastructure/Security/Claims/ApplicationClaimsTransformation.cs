using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

[Service(Profile = ServiceProfiles.WebServer)]
public class ApplicationClaimsTransformation : IClaimsTransformation
{
	private static CriticalSection<UserContextInfo> s_criticalSection = new CriticalSection<UserContextInfo>();

	private readonly IClaimsCacheStore _claimsCacheStore;
	private readonly IUserContextInfoBuilder _contextInfoBuilder;
	private readonly ICustomClaimsBuilder _customClaimsBuilder;

	public ApplicationClaimsTransformation(
		IClaimsCacheStore claimsCacheStore,
		IUserContextInfoBuilder contextInfoBuilder,
		ICustomClaimsBuilder customClaimsBuilder)
	{
		_claimsCacheStore = claimsCacheStore;
		_contextInfoBuilder = contextInfoBuilder;
		_customClaimsBuilder = customClaimsBuilder;
	}

	public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	{
		UserContextInfo userContextInfo = _contextInfoBuilder.GetUserContextInfo(principal);

		// user not logged in - no transformation
		if (userContextInfo == null)
		{
			return await Task.FromResult(principal);
		}

		List<Claim> customClaims = _claimsCacheStore.GetClaims(userContextInfo);
		if (customClaims == null)
		{
			await s_criticalSection.ExecuteActionAsync(userContextInfo, async () =>
			{
				customClaims = _claimsCacheStore.GetClaims(userContextInfo);
				if (customClaims == null)
				{
					customClaims = await _customClaimsBuilder.GetCustomClaimsAsync(principal);
					_claimsCacheStore.StoreClaims(userContextInfo, customClaims);
				}
			});
		}

		// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/claims?view=aspnetcore-7.0#extend-or-add-custom-claims-using-iclaimstransformation
		ClaimsIdentity claimsIdentity = new ClaimsIdentity();
		claimsIdentity.AddClaims(customClaims);
		principal.AddIdentity(claimsIdentity);
		return principal;
	}
}
