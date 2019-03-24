namespace Alpha.Travel.Application.Common.Models
{
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MetaData
    {
        [JsonProperty(Order = -4, PropertyName = "total_records")]
        public int TotalRecords { get; set; }

        [JsonProperty(Order = -3, PropertyName = "page_count")]
        public double PageCount { get; set; }

        [JsonProperty(Order = -2, PropertyName = "page_number")]
        public int PageNumber { get; set; }
    }
}