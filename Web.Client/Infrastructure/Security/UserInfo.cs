using System.Security.Claims;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

// source: https://github.com/dotnet/blazor-samples/tree/main/8.0/BlazorWebAppOidc

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public sealed class UserInfo
{
	// The Claim type is not serializable to JSON, so we will use a custom class (or record).

	public required SerializableClaim[] Claims { get; init; }

	public const string UserIdClaimType = "sub";
	public const string NameClaimType = "name";
	public const string RoleClaimType = ClaimTypes.Role;

	public static UserInfo FromClaimsPrincipal(ClaimsPrincipal principal) =>
		new UserInfo
		{
			// Select Claims that are useful for the client, avoiding sending unnecessary data
			Claims = principal.Claims
				.Where(claim => claim.Type is UserIdClaimType or NameClaimType or RoleClaimType)
				.Select(claim => new SerializableClaim(claim.Type, claim.Value))
				.ToArray()
		};

	public ClaimsPrincipal ToClaimsPrincipal() =>
		new ClaimsPrincipal(new ClaimsIdentity(
			claims: Claims.Select(serializedClaim => new Claim(serializedClaim.Type, serializedClaim.Value)),
			authenticationType: nameof(UserInfo),
			nameType: NameClaimType,
			roleType: RoleClaimType));

	public record SerializableClaim(string Type, string Value);
}
