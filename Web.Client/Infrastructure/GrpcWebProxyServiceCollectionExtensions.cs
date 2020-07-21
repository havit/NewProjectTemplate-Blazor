using Havit.GoranG3.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Havit.GoranG3.Web.Client.Infrastructure
{
	// TODO: Kam s tím?
	public static class GrpcWebProxyServiceCollectionExtensions
	{
		public static void AddGrpcWebProxy<TService>(this IServiceCollection services)
			where TService : class
		{
			// TODO: Parametrizace
			string webAPIConnectionString = "https://localhost:44377/";

			services.AddSingleton<TService>(sp =>
			{
				var handler = new Grpc.Net.Client.Web.GrpcWebHandler(Grpc.Net.Client.Web.GrpcWebMode.GrpcWeb, new HttpClientHandler());
				var channel = Grpc.Net.Client.GrpcChannel.ForAddress(webAPIConnectionString, new Grpc.Net.Client.GrpcChannelOptions() { HttpClient = new HttpClient(handler) });

				return channel.CreateGrpcService<TService>(ClientFactory.Create(BinderConfiguration.Create(null, new GrpcServiceBinder())));
			});
		}
	}
}
