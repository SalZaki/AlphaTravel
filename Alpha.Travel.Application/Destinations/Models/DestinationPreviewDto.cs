namespace Alpha.Travel.Application.Models
{
    using System;
    using System.Linq.Expressions;
    using Domain.Entities;
    using Newtonsoft.Json;

    public class DestinationPreviewDto
    {
        public DestinationPreviewDto() { }

        public static Expression<Func<Destination, DestinationPreviewDto>> Projection
        {
            get
            {
                return c => new DestinationPreviewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedOn = c.ModifiedOn
                };
            }
        }

        [JsonProperty(Order = -8, PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(Order = -7, PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(Order = -6, PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(Order = -5, PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -4, PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(Order = -3, PropertyName = "modified_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -2, PropertyName = "modified_by", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; set; }
    }
}