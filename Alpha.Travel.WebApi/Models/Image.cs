namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;

    public sealed class Image
    {
        [JsonProperty(Order = -6, PropertyName = "alt")]
        public string Alt { get; set; }

        [JsonProperty(Order = -5, PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(Order = -4, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Order = -3, PropertyName = "src")]
        public string Src { get; set; }

        [JsonProperty(Order = -2, PropertyName = "display_order")]
        public int DisplayOrder { get; set; }

        [JsonProperty(Order = -1, PropertyName = "copy_right_text")]
        public string CopyRightText { get; set; }
    }
}