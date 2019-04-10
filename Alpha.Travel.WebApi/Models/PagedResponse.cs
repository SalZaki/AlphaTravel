namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class PagedResponse<T> : BaseResponse where T : class
    {
        [JsonProperty(Order = -5, PropertyName = "data")]
        public IEnumerable<T> Data { get; set; }

        [JsonProperty(Order = -4, PropertyName = "meta_data")]
        public MetaData Metadata { get; set; }

        [JsonProperty(Order = -3, PropertyName = "pagination")]
        public Pagination Pagination { get; set; }
    }
}