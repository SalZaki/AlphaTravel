namespace Alpha.Travel.WebApi.ClientSDK.Models.Response
{
    using Newtonsoft.Json;

    public sealed class Destination : BaseModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}