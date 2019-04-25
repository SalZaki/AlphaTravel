namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public sealed class Destination : BaseModel
    {
        [JsonProperty(Order = -14, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Order = -13, PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(Order = -12, PropertyName = "short_description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty(Order = -11, PropertyName = "average_rating", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal AverageRating { get; set; }

        [JsonProperty(Order = -10, PropertyName = "reviews_allowed", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ReviewsAllowed { get; set; }

        [JsonProperty(Order = -9, PropertyName = "string", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(Order = -8, PropertyName = "featured", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Featured { get; set; }

        [JsonProperty(Order = -7, PropertyName = "meta_data", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> MetaData { get; set; }

        [JsonProperty(Order = -6, PropertyName = "images", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<Image> Images { get; set; }
    }
}