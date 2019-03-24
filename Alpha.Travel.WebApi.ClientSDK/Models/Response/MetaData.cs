namespace Alpha.Travel.WebApi.ClientSDK.Models.Response
{
    using Newtonsoft.Json;

    public sealed class MetaData
    {
        [JsonProperty(Order = -3, PropertyName = "total_records")]
        public int TotalRecords { get; set; }

        [JsonProperty(Order = -2, PropertyName = "page_count")]
        public double PageCount { get; set; }

        [JsonProperty(Order = -1, PropertyName = "page_number")]
        public int PageNumber { get; set; }
    }
}