namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class BaseModel
    {
        [JsonProperty(Order = -99, PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(Order = -4, PropertyName = "modified_on")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -3, PropertyName = "modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty(Order = -2, PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -1, PropertyName = "created_by")]
        public string CreatedBy { get; set; }
    }
}