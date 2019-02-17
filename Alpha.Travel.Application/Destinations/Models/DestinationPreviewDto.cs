namespace Alpha.Travel.Application.Destinations.Models
{
    using System;
    using System.Linq.Expressions;
    using Domain.Entities;

    public class DestinationPreviewDto
    {
        public DestinationPreviewDto() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Destination, DestinationPreviewDto>> Projection
        {
            get
            {
                return c => new DestinationPreviewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                };
            }
        }
    }
}