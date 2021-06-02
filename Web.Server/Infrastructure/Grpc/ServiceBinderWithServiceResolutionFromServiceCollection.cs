using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Havit.NewProjectTemplate.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Grpc
{
	/// <summary>
	/// Allows mapping gRPC endpoints using interfaces (reads metadata from implementation - i.e. [Authorize] attribute)
	/// </summary>
	/// <remarks>
	/// https://github.com/protobuf-net/protobuf-net.Grpc/issues/121
	/// SRC: https://github.com/protobuf-net/protobuf-net.Grpc/blob/main/examples/pb-net-grpc/Server_CS/ServiceBinderWithServiceResolutionFromServiceCollection.cs
	/// </remarks>
	public class ServiceBinderWithServiceResolutionFromServiceCollection : ProtoBufServiceBinder
	{
		private readonly IServiceCollection services;

		public ServiceBinderWithServiceResolutionFromServiceCollection(IServiceCollection services)
		{
			this.services = services;
		}

		public override IList<object> GetMetadata(MethodInfo method, Type contractType, Type serviceType)
		{
			var resolvedServiceType = serviceType;
			if (serviceType.IsInterface)
			{
				resolvedServiceType = services.SingleOrDefault(x => x.ServiceType == serviceType)?.ImplementationType ?? serviceType;
			}

			return base.GetMetadata(method, contractType, resolvedServiceType);
		}
	}
}
