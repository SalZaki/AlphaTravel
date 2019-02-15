namespace Alpha.Travel.Application.Destinations.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Alpha.Travel.Application.Categories.Models;

    public class GetDestinationPreviewQuery : IRequest<List<DestinationPreviewDto>>
    {
        public int DestinationId { get; set; }
    }
}