namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;

    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
