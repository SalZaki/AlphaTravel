namespace Alpha.Travel.WebApi.ClientSDK.Models.Response
{
    using Newtonsoft.Json;

    public sealed class Customer : BaseModel
    {
        [JsonProperty(Order = -9, PropertyName = "first_name")]
        public string Firstname { get; set; }

        [JsonProperty(Order = -8, PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(Order = -7, PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(Order = -6, PropertyName = "email")]
        public string Email { get; set; }
    }
}