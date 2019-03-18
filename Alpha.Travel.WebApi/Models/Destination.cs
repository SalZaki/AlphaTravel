namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System;

    public class Destination : Resource
    {
        [JsonProperty(Order = -8)]
        public int Id { get; set; }

        [JsonProperty(Order = -7)]
        public string Name { get; set; }

        [JsonProperty(Order = -6)]
        public string Description { get; set; }

        [JsonProperty(Order = -5, PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -4, PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(Order = -3, PropertyName = "modified_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -2, PropertyName = "modified_by", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; set; }
    }
}