namespace Alpha.Travel.Application.Destinations.Commands
{
    using Models;
    using MediatR;
    using System;

    public class CreateDestination : IRequest<DestinationPreviewDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}