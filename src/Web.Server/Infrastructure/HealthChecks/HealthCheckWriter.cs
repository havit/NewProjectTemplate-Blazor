using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.HealthChecks;

public static class HealthCheckWriter
{
	public static Task WriteResponseAsync(HttpContext context, HealthReport result)
	{
		context.Response.ContentType = "application/json; charset=utf-8";

		var options = new JsonWriterOptions
		{
			Indented = true
		};

		// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1#use-health-checks-routing

		using var stream = new MemoryStream();
		using (var writer = new Utf8JsonWriter(stream, options))
		{

			writer.WriteStartObject();
			writer.WriteStartArray("Items");

			foreach (var entry in result.Entries)
			{
				writer.WriteStartObject();

				writer.WriteString("Key", entry.Key);
				writer.WriteString("Status", entry.Value.Status.ToString());
				writer.WriteString("Duration", entry.Value.Duration.ToString());

				if (!string.IsNullOrEmpty(entry.Value.Description))
				{
					writer.WriteString("Description", entry.Value.Description);
				}

				if (!string.IsNullOrEmpty(entry.Value.Exception?.Message))
				{
					writer.WriteString("Exception", entry.Value.Exception.Message);
				}

				writer.WriteEndObject();
			}

			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		var json = Encoding.UTF8.GetString(stream.ToArray());

		return context.Response.WriteAsync(json);
	}
}
