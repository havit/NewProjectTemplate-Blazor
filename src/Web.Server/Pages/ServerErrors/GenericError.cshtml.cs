using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Havit.NewProjectTemplate.Web.Server.Pages.ServerErrors;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class GenericErrorModel : PageModel
{
	public string RequestId { get; set; }

	public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

	public void OnGet()
	{
		ViewData["Title"] = "Error";
		RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
	}
}