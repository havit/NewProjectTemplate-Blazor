using System.Net;
using System.Security;

namespace Havit.NewProjectTemplate.Web.Server.Infrastructure.ExceptionHandling;

public class CustomResponseForKnownExceptionsMiddleware
{
	private readonly RequestDelegate _next;

	public CustomResponseForKnownExceptionsMiddleware(RequestDelegate next)
	{
		_next = next;
	}

#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
	public async Task Invoke(HttpContext context)
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
	{
		try
		{
			// call the next middleware
			await _next(context);
		}
		catch (SecurityException ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
			context.Response.ContentType = "text/plain";
			await context.Response.WriteAsync(ex.Message); // this won't be visible in the browser, but might be checked in Fiddler

			// do not rethrow the exception, this middleware handles the response completely
		}
		// you might want to catch other exceptions here

		// do not catch all exceptions, let the default exception handler handle them
	}
}
