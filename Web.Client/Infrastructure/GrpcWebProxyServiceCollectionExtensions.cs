﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Havit.GoranG3.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;

namespace Havit.GoranG3.Web.Client.Infrastructure
{
	// TODO: Kam s tím?
	public static class GrpcWebProxyServiceCollectionExtensions
	{
		public static IHttpClientBuilder AddGrpcClientProxy<TService>(this IServiceCollection services)
			where TService : class
		{
			return services
				.AddCodeFirstGrpcClient<TService>((provider, options) =>
				{
					string backendUrl = GetBackendUrl(provider);

					options.Address = new Uri(backendUrl);
				}).ConfigurePrimaryHttpMessageHandler(provider => new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
		}

		public static IHttpClientBuilder AddAuthorizedGrpcClientProxy<TService>(this IServiceCollection services)
			where TService : class
		{
			return AddGrpcClientProxy<TService>(services)
				.ConfigurePrimaryHttpMessageHandler(provider =>
				{
					string backendUrl = GetBackendUrl(provider);

					var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

					var authorizationHandler = provider.GetRequiredService<AuthorizationMessageHandler>()
						.ConfigureHandler(authorizedUrls: new[] { backendUrl }); // scopes: new[] { "example.read", "example.write" }
					authorizationHandler.InnerHandler = grpcWebHandler;

					return authorizationHandler;
				});
		}

		private static string GetBackendUrl(IServiceProvider provider)
		{
			var config = provider.GetRequiredService<IConfiguration>();
			var backendUrl = config["BackendUrl"];

			// If no address is set then fallback to the current webpage URL
			if (string.IsNullOrEmpty(backendUrl))
			{
				var navigationManager = provider.GetRequiredService<NavigationManager>();
				backendUrl = navigationManager.BaseUri;
			}

			return backendUrl;
		}
	}
}
