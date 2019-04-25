namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Response<T> : BaseResponse where T : class
    {
        [JsonProperty(Order = -4, PropertyName = "data")]
        public T Data { get; set; }

    }
}