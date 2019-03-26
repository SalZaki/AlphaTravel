namespace Alpha.Travel.Application.Common.Models
{
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MetaData
    {
        [JsonProperty(Order = -7, PropertyName = "total_records")]
        public int TotalRecords { get; set; }

        [JsonProperty(Order = -6, PropertyName = "page_count")]
        public double PageCount { get; set; }

        [JsonProperty(Order = -5, PropertyName = "page_number")]
        public int PageNumber { get; set; }

        [JsonProperty(Order = -4, PropertyName = "page_size")]
        public int PageSize { get; set; }

        [JsonProperty(Order = -3, PropertyName = "has_next")]
        public bool HasNext { get; set; }

        [JsonProperty(Order = -2, PropertyName = "has_previous")]
        public bool HasPrevious { get; set; }
    }
}