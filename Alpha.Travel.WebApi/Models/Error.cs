namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;

    public class Error
    {
        [JsonProperty(Order = -5, PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(Order = -4, PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(Order = -3, PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(Order = -2, PropertyName = "documentation_url", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentationUrl { get; set; }
    }
}