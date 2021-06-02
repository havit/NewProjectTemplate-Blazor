using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Havit.ComponentModel;
using Havit.Diagnostics.Contracts;
using Havit.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.Grpc
{
	public static class EndpointRouteBuilderGrpcExtensions
	{
		public static void MapGrpcServicesByApiContractAttributes(this IEndpointRouteBuilder builder, Assembly assemblyToScan)
		{
			Contract.Requires<ArgumentNullException>(builder is not null, nameof(builder));
			Contract.Requires<ArgumentNullException>(assemblyToScan is not null, nameof(assemblyToScan));

			var interfacesAndAttributes = (from type in assemblyToScan.GetTypes()
										   from apiContractAttribute in type.GetCustomAttributes(typeof(ApiContractAttribute), false).Cast<ApiContractAttribute>()
										   select new { Interface = type, Attribute = apiContractAttribute }).ToArray();

			var mapGrpcServiceMethodInfo = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod(nameof(GrpcEndpointRouteBuilderExtensions.MapGrpcService));


			foreach (var item in interfacesAndAttributes)
			{
				var endpoint = (GrpcServiceEndpointConventionBuilder)mapGrpcServiceMethodInfo.MakeGenericMethod(item.Interface).Invoke(null, new[] { builder });
				if (item.Attribute.RequireAuthorization)
				{
					endpoint.RequireAuthorization();
				}
			}
		}
	}
}
