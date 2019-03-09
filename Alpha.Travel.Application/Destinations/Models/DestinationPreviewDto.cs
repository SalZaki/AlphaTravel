namespace Alpha.Travel.Application.Destinations.Models
{
    using System;
    using System.Linq.Expressions;
    using Domain.Entities.Destination;

    public class DestinationPreviewDto
    {
        public DestinationPreviewDto()
        {

        }

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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}