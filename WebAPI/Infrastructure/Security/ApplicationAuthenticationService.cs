using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Facades.Infrastructure.Security.Authentication;
using Havit.GoranG3.Model.Security;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Havit.GoranG3.DataLayer.Repositories.Security;
using Havit.GoranG3.Facades.Infrastructure.Security.Claims;

namespace Havit.GoranG3.WebAPI.Infrastructure.Security
{
    /// <summary>
    /// Poskytuje uživatele z HttpContextu.
    /// </summary>
    public class ApplicationAuthenticationService : IApplicationAuthenticationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

	    private readonly Lazy<LoginAccount> loginAccountLazy; 

		public ApplicationAuthenticationService(IHttpContextAccessor httpContextAccessor, ILoginAccountRepository loginAccountRepository)
        {
            this.httpContextAccessor = httpContextAccessor;

	        loginAccountLazy = new Lazy<LoginAccount>(() => loginAccountRepository.GetObject(GetCurrentClaimsPrincipal().GetLoginAccountId()));
		}

        public ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
			return httpContextAccessor.HttpContext.User;
        }

	    public LoginAccount GetCurrentLoginAccount() => loginAccountLazy.Value;
	}
}
