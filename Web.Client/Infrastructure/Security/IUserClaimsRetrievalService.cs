using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Security;

public interface IUserClaimsRetrievalService
{
	Task<IEnumerable<Claim>> FetchAdditionalUserClaimsAsync(IAccessTokenProvider accessTokenProvider, CancellationToken cancellationToken = default);
}
