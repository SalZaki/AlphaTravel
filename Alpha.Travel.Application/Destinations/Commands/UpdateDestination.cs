namespace Alpha.Travel.Application.Destinations.Commands
{
    using MediatR;
    using System;

    public class UpdateDestination : IRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
