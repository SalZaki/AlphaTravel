namespace Alpha.Travel.WebApi.ClientSDK.Models.Response
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public sealed class PagedResult<T> where T : class
    {
        [JsonProperty(Order = -2, PropertyName = "data")]
        public IList<T> Data { get; set; }

        [JsonProperty(Order = -1, PropertyName = "meta_data")]
        public MetaData MetaData { get; set; }
    }
}