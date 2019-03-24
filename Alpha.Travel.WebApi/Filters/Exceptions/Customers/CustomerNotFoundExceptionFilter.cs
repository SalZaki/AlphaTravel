namespace Alpha.Travel.WebApi.Filters.Exceptions.Destinations
{
    using Application.Common.Enums;
    using WebApi.Results;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;
    using Application.Customers.Exceptions;

    public abstract class BaseExceptionFilter
    {
        public string ErrorTitle { get; }

        public ApiSettings ApiSettings { get; }

        public BaseExceptionFilter(ApiSettings apiSettings, string errorTitle)
        {
            ApiSettings = apiSettings;
            ErrorTitle = errorTitle;
        }
    }

    public sealed class CustomerNotFoundExceptionFilter : BaseExceptionFilter, IExceptionFilter
    {
        public CustomerNotFoundExceptionFilter(IOptionsSnapshot<ApiSettings> apiSettings) :
            base(apiSettings.Value, "Customer Not Found")
        { }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomerNotFoundException)
            {
                var controller = context.RouteData.Values["controller"].ToString().ToLowerInvariant();
                var version = context.RouteData.Values["version"].ToString().ToLowerInvariant();
                var documentationUrl = ApiSettings.ApiDocumentationUrl.Replace("{VERSION}", version) + controller;
                var errorType = Error.InvalidCustomerId.ToString();
                var errorMessage = $"Customer not found exception has occured, bacause invalid customer id was passed.";
                context.Result = new NotFoundResult(ErrorTitle, errorMessage, errorType, documentationUrl);
                context.ExceptionHandled = true;
            }
        }
    }
}