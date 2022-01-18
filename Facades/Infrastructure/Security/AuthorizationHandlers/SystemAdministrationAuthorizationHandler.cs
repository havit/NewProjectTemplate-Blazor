using Havit.Extensions.DependencyInjection.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Facades.Infrastructure.Security.AuthorizationHandlers;

// TODO: Ukázka autorization handleru (negenerického, což není obvyklé). Implementovat nebo odstranit třídu.
[Service(Lifetime = ServiceLifetime.Singleton)]
public class SystemAdministrationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
	{
#if DEBUG
		context.Succeed(requirement);
#endif
		//if ((requirement == Operations.SystemAdministration) && context.User.HasClaim(...))
		//{
		//    context.Succeed(requirement);
		//}

		return Task.CompletedTask;
	}
}
