using System;
using System.Security;
using System.Security.Claims;
using Havit.Diagnostics.Contracts;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Authorization
{
    [Service]
    public class ApplicationAuthorizationService : IApplicationAuthorizationService
    {
        private readonly IApplicationAuthenticationService applicationAuthenticationService;
        private readonly IAuthorizationService authorizationService;

        public ApplicationAuthorizationService(IApplicationAuthenticationService applicationAuthenticationService, IAuthorizationService authorizationService)
        {
            this.applicationAuthenticationService = applicationAuthenticationService;
            this.authorizationService = authorizationService;
        }

        public bool IsAuthorized(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
        {
            Contract.Requires<ArgumentNullException>(user != null, nameof(user));
            Contract.Requires<ArgumentNullException>(requirement != null, nameof(requirement));
            
            return authorizationService.AuthorizeAsync(user, resource, requirement).GetAwaiter().GetResult().Succeeded;
        }

        public void VerifyAuthorization(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
        {
            if (!IsAuthorized(user, requirement, resource))
            {
                throw new SecurityException();
            }
        }

        public bool IsCurrentUserAuthorized(IAuthorizationRequirement requirement, object resource = null)
        {
            return IsAuthorized(applicationAuthenticationService.GetCurrentClaimsPrincipal(), requirement, resource);
        }

        public void VerifyCurrentUserAuthorization(IAuthorizationRequirement requirement, object resource = null)
        {
            VerifyAuthorization(applicationAuthenticationService.GetCurrentClaimsPrincipal(), requirement, resource);
        }

        public bool IsCurrentUserAuthorized(IAuthorizationRequirement requirement)
        {
            return IsAuthorized(applicationAuthenticationService.GetCurrentClaimsPrincipal(), null, requirement);
        }
    }
}
