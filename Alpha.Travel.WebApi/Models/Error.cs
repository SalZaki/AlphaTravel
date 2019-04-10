namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Error
    {
        [JsonProperty(Order = -4, PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(Order = -3, PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(Order = -2, PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(Order = -1, PropertyName = "documentation_url")]
        public string DocumentationUrl { get; set; }
    }
}