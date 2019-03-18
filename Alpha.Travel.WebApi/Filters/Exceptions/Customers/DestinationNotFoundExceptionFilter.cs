namespace Alpha.Travel.WebApi.Filters.Exceptions.Destinations
{
    using Application.Common.Enums;
    using WebApi.Results;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;
    using Alpha.Travel.Application.Customers.Exceptions;

    public class CustomerNotFoundExceptionFilter : IExceptionFilter
    {
        private const string ERORR_TITLE = "Not Found Error";
        private readonly ApiSettings _apiSettings;

        public CustomerNotFoundExceptionFilter(IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomerNotFoundException)
            {
                var controller = context.RouteData.Values["controller"].ToString().ToLowerInvariant();
                var version = context.RouteData.Values["version"].ToString().ToLowerInvariant();
                var documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", version) + controller;
                var errorType = Error.InvalidCustomerId.ToString();
                var errorMessage = $"Customer not found exception has occured, bacause invalid customer id was passed.";
                context.Result = new NotFoundResult(ERORR_TITLE, errorMessage, errorType, documentationUrl);
                context.ExceptionHandled = true;
            }
        }
    }
}