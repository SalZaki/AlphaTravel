namespace Alpha.Travel.WebApi.Filters.Exceptions.Destination
{
    using Application.Common.Enums;
    using Application.Destinations.Exceptions;
    using WebApi.Results;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;

    public sealed class DestinationNotFoundExceptionFilter : BaseExceptionFilter, IExceptionFilter
    {
        public DestinationNotFoundExceptionFilter(IOptionsSnapshot<ApiSettings> apiSettings) :
            base(apiSettings.Value, "Destination Not Found")
        { }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DestinationNotFoundException)
            {
                var controller = context.RouteData.Values["controller"].ToString().ToLowerInvariant();
                var version = context.RouteData.Values["version"].ToString().ToLowerInvariant();
                var documentationUrl = ApiSettings.ApiDocumentationUrl.Replace("{VERSION}", version) + controller;
                var errorType = Error.InvalidDestinationId.ToString();
                var errorMessage = $"Destination not found exception has occured, bacause invalid destination id was passed.";
                context.Result = new NotFoundResult(ErrorTitle, errorMessage, errorType, documentationUrl);
                context.ExceptionHandled = true;
            }
        }
    }
}