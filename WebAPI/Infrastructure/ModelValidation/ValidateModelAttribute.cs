using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Havit.GoranG3.WebAPI.Infrastructure.ModelValidation
{
	/// <summary>
	/// Pokud není ModelState validní, vrací odpověď (ValidationResultModel) bez dalšího zpracování action.
	/// </summary>
	/// <remarks>
	/// Vracené fields jsou PascalCase - vychází z pojmenování v .NETu, nikoliv z pojmenování použitého JSON formatterem.
	/// </remarks>
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		public delegate int StatusCodeSelectorDelegate(ModelStateDictionary modelStateDictionary);
		public delegate object ResultSelectorDelegate(int statusCode, ModelStateDictionary modelStateDictionary);

		public StatusCodeSelectorDelegate StatusCodeSelector { get; set; } = DefaultStatusCodeSelector;
	    public ResultSelectorDelegate ResultSelector { get; set; } = ValidationResultModel.FromModelState;

		public override void OnActionExecuting(ActionExecutingContext context)
	    {			
		    if (!context.ModelState.IsValid)
		    {
			    int statusCode = StatusCodeSelector(context.ModelState);
			    object result = ResultSelector(statusCode, context.ModelState);

				context.Result = new ObjectResult(result) { StatusCode = statusCode };
		    }
	    }

		public static int DefaultStatusCodeSelector(ModelStateDictionary modelStateDictionary)
		{
			return modelStateDictionary.Values.Any(item => item.Errors.Any(error => error.Exception != null))
				? StatusCodes.Status500InternalServerError
				: StatusCodes.Status422UnprocessableEntity;
		}
	}
}
