using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Havit.AspNetCore.Mvc.ErrorToJson.Configuration;
using Havit.AspNetCore.Mvc.ExceptionMonitoring.Filters;
using Havit.GoranG3.Services.Infrastructure;
using Havit.GoranG3.WebAPI.Infrastructure.ModelValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
	public static class ErrorToJsonConfig
	{
		public static void AddCustomizedErrorToJson(this IServiceCollection services)
		{
			services.AddErrorToJson(c =>
			{
				c.Map(e => e is SecurityException, e => StatusCodes.Status403Forbidden, ValidationErrorModel.FromException(StatusCodes.Status403Forbidden), markExceptionAsHandled: e => true);
				c.Map(e => e is OperationFailedException, e => StatusCodes.Status422UnprocessableEntity, ValidationErrorModel.FromException(StatusCodes.Status422UnprocessableEntity), markExceptionAsHandled: e => true);
				c.Map(e => true /* ostatní výjimky */, e => StatusCodes.Status500InternalServerError, ValidationErrorModel.FromException(StatusCodes.Status500InternalServerError), markExceptionAsHandled: e => false);
			});
		}
	}
}
