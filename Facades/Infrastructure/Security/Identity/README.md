# Identity Stores Implementation

## Documentation
* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model
* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers


## EF Core implementation:
* https://github.com/aspnet/Identity/blob/d4d105d5b529c8e1701010cb49bc115f0aa23ed0/src/Microsoft.AspNetCore.Identity.EntityFrameworkCore/UserStore.cs
* https://github.com/dotnet/aspnetcore/tree/master/src/Identity/Extensions.Stores/src

## MVC vs EF
* https://knowledge-base.havit.cz/2014/11/27/asp-net-identity-2-1-do-mvc-projektu-s-vlastni-implementaci-userstore-pres-repositories/

## Roles vs. IdentityServer vs. Blazor WebAssembly
* https://github.com/IdentityServer/IdentityServer4/issues/1786
* https://stackoverflow.com/questions/53194947/configuring-identity-server-to-use-asp-net-identity-roles
* https://docs.microsoft.com/en-us/aspnet/core/security/blazor/webassembly/hosted-with-identity-server?view=aspnetcore-3.1
  * https://github.com/dotnet/AspNetCore.Docs/issues/17649
  * https://github.com/dotnet/AspNetCore.Docs/issues/17317
* https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/