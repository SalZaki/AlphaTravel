namespace Alpha.Travel.WebApi.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class BaseModel : Resource
    {
        [JsonProperty(Order = -99, PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(Order = -5, PropertyName = "modified_on")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -4, PropertyName = "modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty(Order = -3, PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -2, PropertyName = "created_by")]
        public string CreatedBy { get; set; }
    }

    public interface IResource
    {
        void AddLink(Link link);
    }

    public abstract class Resource : IResource
    {
        private readonly List<Link> _links;

        [JsonProperty(Order = -1, PropertyName = "_links", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<Link> Links { get { return _links; } }

        public Resource()
        {
            _links = new List<Link>();
        }

        public void AddLink(Link link)
        {
            // Ensure.Argument.NotNull(link, "link");
            _links.Add(link);
        }

        public void AddLinks(IEnumerable<Link> links)
        {
            _links.AddRange(links);
        }
    }
}