using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Havit.NewProjectTemplate.Web.Client.Infrastructure.Configuration;

public static class WebAssemblyHostBuilderExtensions
{
	public static async Task<WebAssemblyHostBuilder> AddJsonStreamAsync(this WebAssemblyHostBuilder builder, string configurationUri, CancellationToken cancellationToken = default)
	{
		var http = new HttpClient()
		{
			BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
		};

		using var response = await http.GetAsync(configurationUri, cancellationToken);
		using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

		builder.Configuration.AddJsonStream(stream);

		return builder;
	}
}
