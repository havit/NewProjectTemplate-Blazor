using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Havit.Diagnostics.Contracts;
using Havit.Extensions.DependencyInjection.Abstractions;
using Havit.GoranG3.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Claims
{
    /// <summary>
    /// Přidá do claims z JWT další claims.    
    /// Jako side-efekt (v implementacích) zajistí založení LoginAccountu, pokud ještě neexistuje.
    /// Optimalizace: Claims jsou drženy v cache, aby je nebylo nutné každý request skládat znovu a znovu.
    /// </summary>
    [Service(Profile = ServiceProfiles.WebAPI)]
	public class ApplicationClaimsTransformation : IClaimsTransformation // TODO: Vyžaduje závislost na Microsoft.AspNetCore.Authentication (který je však naposled ve verzi 2.2.0), výhledově proto přesuneme třídu do WebAPI.
	{
        private readonly IClaimsCacheStore claimsCacheStore;
	    private readonly IUserContextInfoBuilder contextInfoBuilder;
	    private readonly ICustomClaimsBuilder customClaimsBuilder;

        public ApplicationClaimsTransformation(IClaimsCacheStore claimsCacheStore, IUserContextInfoBuilder contextInfoBuilder, ICustomClaimsBuilder customClaimsBuilder)
        {
            this.claimsCacheStore = claimsCacheStore;
	        this.contextInfoBuilder = contextInfoBuilder;
	        this.customClaimsBuilder = customClaimsBuilder;
        }

	    /// <summary>
	    /// Transformuje claims principla - přidá custom claims.
	    /// </summary>
	    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	    {
		    UserContextInfo userContextInfo = contextInfoBuilder.GetUserContextInfo(principal);

		    // pro nepřihlášeného uživatele žádnou transformaci neprovádíme
		    if (userContextInfo == null)
		    {
			    return Task.FromResult(principal);
		    }

		    List<Claim> customClaims = claimsCacheStore.GetClaims(userContextInfo);
		    if (customClaims == null)
		    {
			    // pokud nejsou žádné claims v cache, získáme je a do cache uložíme,
			    // ať je pro stejný context další request rychlejší
			    customClaims = customClaimsBuilder.GetCustomClaims(principal);
			    claimsCacheStore.StoreClaims(userContextInfo, customClaims);
		    }

			// zduplikujeme identity a claims tak, že k duplikátům přidáme custom claim
			// pokud bychom přidávali claims do principal, který je parametrem, může se stát (a stalo se!), že se v identity objeví claims vícekrát
		    ClaimsIdentity claimsIdentity = ((ClaimsIdentity)principal.Identity).Clone();
			claimsIdentity.AddClaims(customClaims);
		    ClaimsPrincipal claimsPrincipalWithCustomClaims = new ClaimsPrincipal(claimsIdentity);
		    return Task.FromResult(claimsPrincipalWithCustomClaims);
	    }
    }
}
