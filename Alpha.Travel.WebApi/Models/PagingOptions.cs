namespace Alpha.Travel.WebApi.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class PagingOptions
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}