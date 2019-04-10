namespace Alpha.Travel.WebApi.Results
{
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Models;

    [ExcludeFromCodeCoverage]
    public sealed class BadRequestResult : IActionResult
    {
        private readonly string _errorTitle;
        private readonly string _errorMessage;
        private readonly string _errorType;
        private readonly string _documentationUrl;

        public BadRequestResult(
            string errorTitle,
            string errorMessage,
            string errorType,
            string documentationUrl)
        {
            _errorTitle = errorTitle;
            _errorMessage = errorMessage;
            _errorType = errorType;
            _documentationUrl = documentationUrl;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var error = new Error
            {
                Title = _errorTitle,
                Message = _errorMessage,
                Type = _errorType,
                DocumentationUrl = _documentationUrl
            };

            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(error));
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.HttpContext.Response.ContentType = "application/json";
            return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}