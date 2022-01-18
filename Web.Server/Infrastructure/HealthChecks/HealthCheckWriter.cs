using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.HealthChecks;

public class HealthCheckWriter
{
	public static Task WriteResponse(HttpContext context, HealthReport result)
	{
		context.Response.ContentType = "application/json";

		// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1#use-health-checks-routing

		var json = new JObject(new JProperty("Items", result.Entries.Select(entry =>
		{
			var result = new JObject(
			new JProperty("Key", entry.Key),
			new JProperty("Status", entry.Value.Status.ToString()),
			new JProperty("Duration", entry.Value.Duration.ToString()));

			if (!String.IsNullOrEmpty(entry.Value.Description))
			{
				result.Add(new JProperty("Description", entry.Value.Description));
			}

			if (!String.IsNullOrEmpty(entry.Value.Exception?.Message))
			{
				result.Add(new JProperty("Exception", entry.Value.Exception?.Message ?? string.Empty));
			}

			return result;
		})));

		return context.Response.WriteAsync(json.ToString(Formatting.Indented));
	}
}
