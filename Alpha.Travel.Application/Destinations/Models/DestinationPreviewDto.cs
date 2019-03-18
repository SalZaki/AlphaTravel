namespace Alpha.Travel.Application.Models
{
    using System;
    using System.Linq.Expressions;
    using Domain.Entities;

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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}