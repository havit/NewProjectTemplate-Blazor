using System;
using System.Collections.Generic;
using Havit.GoranG3.WebAPI.Infrastructure.ModelValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TypeLite;

namespace Havit.GoranG3.WebAPI.Infrastructure.ConfigurationExtensions
{
	/// <summary>
	/// Třída popisuje validační chybu WebAPI.
	/// </summary>
	[TsClass(Module = "ValidationErrors", Name = "ValidationError")]
	public class ValidationErrorModel
	{
		/// <summary>
		/// Status code.
		/// </summary>
		public int StatusCode { get; private set; }

		/// <summary>
		/// Text chyby. Použito pro chyby vyhozené "ručně" výjimkou OperationFailedException (ev. jiné).
		/// Null hodnota není do JSON serializována.
		/// Je vzájemně výlučné s Errors. Buď je jedno, nebo druhé.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Message { get; private set; }

#if DEBUG
		/// <summary>
		/// Stack trace výjimky. Použito pro chyby vyhozené "ručně" výjimkou OperationFailedException (ev. jiné). Jen pro aplikaci kompilovanou v DEBUGu!
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]		
		[TsIgnore]
		public string StackTrace { get; private set; }
#endif

		/// <summary>
		/// Konstruktor.
		/// </summary>
		private ValidationErrorModel(int statusCode)
		{
			this.StatusCode = statusCode;
		}

		/// <summary>
		/// Chyby validace modelu.
		/// Null hodnota není do JSON serializována.
		/// Je vzájemně výlučné s Message. Buď je jedno, nebo druhé.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IReadOnlyCollection<FieldValidationError> Errors { get; private set; }

		/// <summary>
		/// Vrací ValidationErrorModel pro výjimku.
		/// </summary>
		public static Func<Exception, ValidationErrorModel> FromException(int statusCode)
		{
			return (Exception e) => new ValidationErrorModel(statusCode)
			{
				Message = e.Message,
#if DEBUG
				StackTrace = e.StackTrace
#endif
			};
		}

		public delegate ValidationErrorModel ValidateErrorModelSelectorDelegate(int statusCode, ModelStateDictionary modelState);

		/// <summary>
		/// Vrací ValidationErrorModel pro ModelState.
		/// </summary>
		public static ValidateModelAttribute.ResultSelectorDelegate FromModelState()
		{
			return (int statusCode, ModelStateDictionary modelState) => new ValidationErrorModel(statusCode)
			{
				Errors = FieldValidationError.FromModelState(modelState)
			};
		}
	}
}