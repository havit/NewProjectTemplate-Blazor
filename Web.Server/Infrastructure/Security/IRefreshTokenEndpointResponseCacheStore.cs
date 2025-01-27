using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Security;

public interface IRefreshTokenEndpointResponseCacheStore
{
	OpenIdConnectMessage GetResponse(string refreshToken);
	void StoreResponse(string refreshToken, OpenIdConnectMessage response);
}