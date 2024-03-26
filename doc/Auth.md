# Authentication and authorization

## Pre-baked functionality

* AzureAD authentication
* JWT-based auth of Web.Client calls to Web.Server API (gRPC)
* OIDC-based auth of Web.Server content (e.g. Hangfire Dashboard)
* application User with Roles (domain entities) incl. enrichment of the claims and propagation of roles to Web.Client
* onboarding of first user and assigning all roles to him (only in `DEBUG` if not adjusted)

## How-to's

### Add custom claims

Adjust the `CustomClaimsBuilder.GetCustomClaimsAsync()` implementation. There already are some claims being added (roles, UserId, ...). If you want the new claims to be available on Web.Client, you have to propagate them (see below).

### Propagate server claims to Web.Client

Web.Client (`RolesAccountClaimsPrincipalFactory`) asks Web.Server (REST API call to /user-claims) for additional claims when building the user identity.

Adjust the `UserProfileController.GetAdditionalClaims()` method (Web.Server/Controllers). There already are some claims being sent to Web.Client.

### Web.Server: Authenticating server-served content

Default server authentication scheme is `JwtBearerDefaults.AuthenticationScheme` which is used for incoming API calls. All the server-served content which is to be authenticated using OIDC (such as Hangfire Dashboard, or any server-based MVC/RazorPages) has to be explicitly switched to `OpenIdConnectDefaults.AuthenticationScheme`

* with attribute `[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]`
* or with policy `policy.AuthenticationSchemes.Add(OpenIdConnectDefaults.AuthenticationScheme);` (see HangfireDashboard setup for reference)

Also see https://learn.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme for details.





