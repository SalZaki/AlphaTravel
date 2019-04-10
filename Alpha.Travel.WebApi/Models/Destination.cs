namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;

    public sealed class Destination : BaseModel
    {
        [JsonProperty(Order = -6, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Order = -5, PropertyName = "description")]
        public string Description { get; set; }
    }
}