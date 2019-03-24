namespace Alpha.Travel.WebApi.Filters.Exceptions
{
    using System;
    using System.Linq;
    using Results;
    using FluentValidation;
    using Application.Common.Enums;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;

    public class ValidationExceptionFilter : IExceptionFilter
    {
        private const string ERORR_TITLE = "Validation Error";

        private readonly ApiSettings _apiSettings;

        public ValidationExceptionFilter(IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;
            if (exception != null)
            {
                var controller = context.RouteData.Values["controller"].ToString().ToLowerInvariant();
                var version = context.RouteData.Values["version"].ToString().ToLowerInvariant();
                var documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", version) + controller;
                var failure = exception.Errors.First();
                var errorType = (Error)Enum.Parse(typeof(Error), failure.ErrorCode, true);
                var errorMessage = $"Validation failed on '{failure.PropertyName}' property, bacause '{failure.ErrorMessage}'";
                context.Result = new BadRequestResult(ERORR_TITLE, errorMessage, errorType.ToString(), documentationUrl);
                context.ExceptionHandled = true;
            }
        }
    }
}