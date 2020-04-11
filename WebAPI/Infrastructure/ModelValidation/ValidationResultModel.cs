using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Havit.GoranG3.WebAPI.Infrastructure.ModelValidation
{
	public class ValidationResultModel
	{
		public int StatusCode { get; }

		public ReadOnlyCollection<FieldValidationError> Errors { get; }

		public ValidationResultModel(int statusCode, ModelStateDictionary modelState)
		{
			this.StatusCode = statusCode;
			this.Errors = FieldValidationError.FromModelState(modelState);
		}

		public static object FromModelState(int statusCode, ModelStateDictionary modelState)
		{
			return new ValidationResultModel(statusCode, modelState);
		}
	}
}
