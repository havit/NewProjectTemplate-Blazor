using System;
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
using ProtoBuf.Grpc.Configuration;

namespace Havit.GoranG3.Web.Client.Infrastructure
{
	// TODO: Kam s tím?
	public static class GrpcWebProxyServiceCollectionExtensions
	{
		public static void AddGrpcChannel(this IServiceCollection services)
		{
			// Credits: https://github.com/grpc/grpc-dotnet/blob/master/examples/Blazor/Client/Program.cs
			services.AddScoped(services =>
			{
				// Get the service address from appsettings.json
				var config = services.GetRequiredService<IConfiguration>();
				var backendUrl = config["BackendUrl"];

				// If no address is set then fallback to the current webpage URL
				if (string.IsNullOrEmpty(backendUrl))
				{
					var navigationManager = services.GetRequiredService<NavigationManager>();
					backendUrl = navigationManager.BaseUri;
				}


				// Create a channel with a GrpcWebHandler that is addressed to the backend server.
				//
				// GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
				// then GrpcWeb is recommended because it produces smaller messages.
				var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

				var authorizationMessageHandler = services.GetRequiredService<AuthorizationMessageHandler>()
					.ConfigureHandler(authorizedUrls: new[] { backendUrl }); // scopes: new[] { "example.read", "example.write" }
				authorizationMessageHandler.InnerHandler = grpcWebHandler;

				return GrpcChannel.ForAddress(
					backendUrl,
					new GrpcChannelOptions
					{
						// HttpHandler = httpHandler,
						// CompressionProviders = ...,
						// Credentials = ...,
						// DisposeHttpClient = ...,
						HttpClient = new HttpClient(authorizationMessageHandler),
						// LoggerFactory = ...,
						// MaxReceiveMessageSize = ...,
						// MaxSendMessageSize = ...,
						// ThrowOperationCanceledOnCancellation = ...,
					});
			});
		}

		public static void AddGrpcWebProxy<TService>(this IServiceCollection services)
			where TService : class
		{
			services.AddTransient<TService>(services =>
			{
				var grpcChannel = services.GetRequiredService<GrpcChannel>();
				return grpcChannel.CreateGrpcService<TService>(ClientFactory.Create(BinderConfiguration.Create(null, new GrpcServiceBinder())));
			});
		}
	}
}
