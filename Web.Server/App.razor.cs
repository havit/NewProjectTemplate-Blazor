using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace Havit.NewProjectTemplate.Web.Server;
public partial class App
{
	[Inject] protected IOptions<TelemetryConfiguration> TelemetryConfigurationOptions { get; set; }
	[Inject] protected IHttpContextAccessor HttpContextAccessor { get; set; }
	[Inject] protected IFileVersionProvider FileVersionProvider { get; set; }

	// source: https://learn.microsoft.com/en-us/answers/questions/1493923/asp-append-version-true-in-blazor-net-8
	private static readonly PathString pathBase = new PathString("/");

	private string GetFileVersionPath(string path)
	{
		return FileVersionProvider.AddFileVersionToPath(pathBase, path);
	}
}