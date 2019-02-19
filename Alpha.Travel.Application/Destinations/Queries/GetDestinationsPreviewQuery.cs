namespace Alpha.Travel.Application.Destinations.Queries
{
    using System.Collections.Generic;

    using MediatR;
    using Destinations.Models;

    public class GetDestinationsPreviewQuery : IRequest<List<DestinationPreviewDto>>
    {
    }
}