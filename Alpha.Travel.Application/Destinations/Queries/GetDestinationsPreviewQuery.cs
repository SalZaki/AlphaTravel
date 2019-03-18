namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Models;

    public class GetDestinationsPreviewQuery : IRequest<PagedResults<DestinationPreviewDto>>
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public string OrderBy { get; set; }

        public string Query { get; set; }
    }
}