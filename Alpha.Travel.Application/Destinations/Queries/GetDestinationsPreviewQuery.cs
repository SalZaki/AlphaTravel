namespace Alpha.Travel.Application.Destinations.Queries
{
    using Models;
    using MediatR;
    using Common.Queries;
    using System.Collections.Generic;

    public class GetDestinationsPreviewQuery : BaseGetPreviewQuery, IRequest<IList<DestinationPreviewDto>> { }
}