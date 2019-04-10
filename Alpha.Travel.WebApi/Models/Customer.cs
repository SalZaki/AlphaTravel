namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Customer : BaseModel
    {
        [JsonProperty(Order = -8, PropertyName = "first_name")]
        public string Firstname { get; set; }

        [JsonProperty(Order = -7, PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(Order = -6, PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(Order = -5, PropertyName = "email")]
        public string Email { get; set; }
    }
}