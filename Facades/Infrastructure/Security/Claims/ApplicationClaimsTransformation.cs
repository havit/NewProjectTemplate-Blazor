using System.Security.Claims;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.NewProjectTemplate.Services.Infrastructure;
using Havit.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Claims;

[Service(Profile = ServiceProfiles.WebServer)]
public class ApplicationClaimsTransformation : IClaimsTransformation
{
	private static CriticalSection<UserContextInfo> criticalSection = new CriticalSection<UserContextInfo>();

	private readonly IClaimsCacheStore claimsCacheStore;
	private readonly IUserContextInfoBuilder contextInfoBuilder;
	private readonly ICustomClaimsBuilder customClaimsBuilder;

	public ApplicationClaimsTransformation(
		IClaimsCacheStore claimsCacheStore,
		IUserContextInfoBuilder contextInfoBuilder,
		ICustomClaimsBuilder customClaimsBuilder)
	{
		this.claimsCacheStore = claimsCacheStore;
		this.contextInfoBuilder = contextInfoBuilder;
		this.customClaimsBuilder = customClaimsBuilder;
	}

	public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	{
		UserContextInfo userContextInfo = contextInfoBuilder.GetUserContextInfo(principal);

		// user not logged in - no transformation
		if (userContextInfo == null)
		{
			return await Task.FromResult(principal);
		}

		List<Claim> customClaims = claimsCacheStore.GetClaims(userContextInfo);
		if (customClaims == null)
		{
			await criticalSection.ExecuteActionAsync(userContextInfo, async () =>
			{
				customClaims = claimsCacheStore.GetClaims(userContextInfo);
				if (customClaims == null)
				{
					customClaims = await customClaimsBuilder.GetCustomClaimsAsync(principal);
					claimsCacheStore.StoreClaims(userContextInfo, customClaims);
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
