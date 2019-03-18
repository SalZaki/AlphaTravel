namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Models;

    public class GetDestinationPreviewQuery : IRequest<DestinationPreviewDto>
    {
        public string Id { get; set; }
    }
}