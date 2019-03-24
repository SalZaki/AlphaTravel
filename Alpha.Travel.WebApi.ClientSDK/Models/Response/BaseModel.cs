namespace Alpha.Travel.WebApi.ClientSDK.Models.Response
{
    using System;
    using Newtonsoft.Json;

    public abstract class BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "modified_on")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(PropertyName = "modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public string CreatedBy { get; set; }
    }
}