namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Destinations.Models;
    using System.Collections.Generic;

    public class GetDestinationsPreviewQuery : IRequest<List<DestinationPreviewDto>>
    {
    }
}