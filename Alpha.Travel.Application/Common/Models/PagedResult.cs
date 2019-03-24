namespace Alpha.Travel.Application.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class PagedResult<T> where T : class
    {
        [JsonProperty(Order = -3, PropertyName = "data")]
        public IEnumerable<T> Data { get; set; }

        [JsonProperty(Order = -2, PropertyName = "meta_data")]
        public MetaData MetaData { get; set; }
    }
}