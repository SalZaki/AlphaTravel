namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System;

    public class Customer : Resource
    {
        [JsonProperty(Order = -10)]
        public int Id { get; set; }

        [JsonProperty(Order = -9)]
        public string Firstname { get; set; }

        [JsonProperty(Order = -8)]
        public string Surname { get; set; }

        [JsonProperty(Order = -7)]
        public string Password { get; set; }

        [JsonProperty(Order = -6)]
        public string Email { get; set; }

        [JsonProperty(Order = -5, PropertyName = "created_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -4, PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(Order = -3, PropertyName = "modified_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -2, PropertyName = "modified_by", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; set; }
    }
}