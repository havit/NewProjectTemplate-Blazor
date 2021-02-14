using System.Security.Claims;
using Havit.Data.Patterns.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.Authorization.Fakes
{
	/// <summary>
	/// Implementace IApplicationAuthorizationService pro účely testů. Veškerá testz na oprávnění procházejí.
	/// </summary>
	[Fake]
	public class FakeApplicationAuthorizationService : IApplicationAuthorizationService
	{
		public void VerifyAuthorization(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
		{
			// NOOP
		}

		public void VerifyCurrentUserAuthorization(IAuthorizationRequirement requirement, object resource = null)
		{
			// NOOP
		}

		public bool IsAuthorized(ClaimsPrincipal user, IAuthorizationRequirement requirement, object resource = null)
		{
			return true;
		}

		public bool IsCurrentUserAuthorized(IAuthorizationRequirement requirement, object resource = null)
		{
			return true;
		}
	}
}
