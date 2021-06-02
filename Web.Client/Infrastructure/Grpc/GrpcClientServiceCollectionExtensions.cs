using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Grpc.Net.ClientFactory;
using Havit.Blazor.Components.Web;
using Havit.ComponentModel;
using Havit.NewProjectTemplate.Contracts;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc;
using Havit.NewProjectTemplate.Web.Client.Infrastructure.Interceptors;
using Havit.NewProjectTemplate.Web.Client.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Meta;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Grpc
{
	public static class GrpcClientServiceCollectionExtensions
	{
		private static string backendUrl;

		public static void AddGrpcClientInfrastructure(this IServiceCollection services)
		{
			services.AddTransient<AuthorizationGrpcClientInterceptor>();
			services.AddTransient<ServerExceptionsGrpcClientInterceptor>();
			services.AddTransient<GrpcWebHandler>(provider => new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
			services.AddSingleton<ClientFactory>(ClientFactory.Create(BinderConfiguration.Create(marshallerFactories: new[] { ProtoBufMarshallerFactory.Create(RuntimeTypeModel.Create().RegisterApplicationContracts()) }, binder: new ProtoBufServiceBinder())));
		}

		public static void AddGrpcClientsByApiContractAttributes(this IServiceCollection services, Assembly assemblyToScan)
		{
			var interfacesAndAttributes = (from type in assemblyToScan.GetTypes()
										   from apiContractAttribute in type.GetCustomAttributes(typeof(ApiContractAttribute), false).Cast<ApiContractAttribute>()
										   select new { Interface = type, Attribute = apiContractAttribute }).ToArray();

			var addCodeFirstGrpcClientMethodInfo = typeof(ProtoBuf.Grpc.ClientFactory.ServicesExtensions)
				.GetMethod(nameof(ProtoBuf.Grpc.ClientFactory.ServicesExtensions.AddCodeFirstGrpcClient), new[] { typeof(IServiceCollection), typeof(Action<IServiceProvider, GrpcClientFactoryOptions>) });

			Action<IServiceProvider, GrpcClientFactoryOptions> configureClientAction = (provider, options) =>
			{
				options.Address = new Uri(GetBackendUrl(provider));
			};

			foreach (var item in interfacesAndAttributes)
			{
				// services.AddCodeFirstGrpcClient<TService>(configureClientAction)
				var grpcClient = (IHttpClientBuilder)addCodeFirstGrpcClientMethodInfo.MakeGenericMethod(item.Interface)
					.Invoke(null, new object[] { services, configureClientAction });

				grpcClient
					.ConfigurePrimaryHttpMessageHandler<GrpcWebHandler>()
					.AddInterceptor<ServerExceptionsGrpcClientInterceptor>();

				if (item.Attribute.RequireAuthorization)
				{
					grpcClient.AddHttpMessageHandler(provider =>
					{
						return provider.GetRequiredService<AuthorizationMessageHandler>()
							.ConfigureHandler(authorizedUrls: new[] { GetBackendUrl(provider) }); // scopes: new[] { "example.read", "example.write" }
					})
					.AddInterceptor<AuthorizationGrpcClientInterceptor>();
				}
			}
		}

		private static string GetBackendUrl(IServiceProvider provider)
		{
			if (backendUrl == null)
			{
				var config = provider.GetRequiredService<IConfiguration>();
				backendUrl = config["BackendUrl"];

				// If no address is set then fallback to the current webpage URL
				if (string.IsNullOrEmpty(backendUrl))
				{
					var navigationManager = provider.GetRequiredService<NavigationManager>();
					backendUrl = navigationManager.BaseUri;
				}
			}

			return backendUrl;
		}
	}
}
