namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class BaseResponse
    {
        [JsonProperty(Order = -2, PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(Order = -1, PropertyName = "version")]
        public string Version { get; set; }
    }
}