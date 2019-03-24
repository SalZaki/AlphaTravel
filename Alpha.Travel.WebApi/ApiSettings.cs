namespace Alpha.Travel.WebApi
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class ApiSettings
    {
        public int DefaultPageNumber { get; set; }

        public int DefaultPageSize { get; set; }

        public string ApiDocumentationUrl { get; set; }
    }
}