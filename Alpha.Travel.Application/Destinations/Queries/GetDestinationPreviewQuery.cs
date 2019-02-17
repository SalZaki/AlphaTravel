namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Alpha.Travel.Application.Categories.Models;

    public class GetDestinationPreviewQuery : IRequest<DestinationPreviewDto>
    {
        public int Id { get; set; }
    }
}