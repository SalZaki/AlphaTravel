namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Destinations.Models;
    using Common.Models.Responses;

    public class GetDestinationPreviewQuery : IRequest<DestinationResponse>
    {
        public int Id { get; set; }
    }
}