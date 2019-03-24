namespace Alpha.Travel.Application.Destinations.Queries
{
    using Models;
    using MediatR;
    using Common.Queries;

    public class GetDestinationsPreviewQuery : BaseGetPreviewQuery, IRequest<Common.Models.PagedResult<DestinationPreviewDto>> { }
}