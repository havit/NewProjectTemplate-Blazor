using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client.Web;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Havit.GoranG3.Web.Client.Resources;
using Microsoft.Extensions.Logging;

namespace Havit.GoranG3.Web.Client.Infrastructure.Grpc
{
	public class ApplicationGrpcWebHandler : DelegatingHandler
	{
		private readonly IHxMessengerService messenger;
		private readonly IGlobalLocalizer globalLocalizer;
		private readonly ILogger<ApplicationGrpcWebHandler> logger;

		// do not inject scoped services here, the HttpClient stack uses separate scope (different from calling-site scope)
		public ApplicationGrpcWebHandler(
			GrpcWebHandler innerHandler,
			IHxMessengerService messenger,
			IGlobalLocalizer globalLocalizer,
			ILogger<ApplicationGrpcWebHandler> logger)
			: base(innerHandler)
		{
			this.messenger = messenger;
			this.globalLocalizer = globalLocalizer;
			this.logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);

			if (response.Headers.TryGetValues(nameof(OperationFailedException).ToLower(), out var values))
			{
				string errorMessage = String.Join(Environment.NewLine, values);
				logger.LogWarning($"{nameof(OperationFailedException)}: {errorMessage}");
				messenger.AddError(globalLocalizer.OperationFailedExceptionMessengerTitle, errorMessage);
				// the OperationFailedException itself is thrown from ServerExceptionsGrpcClientInterceptor (throwing here would result in RpcException)
			}

			return response;
		}
	}
}
