namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Destinations.Models;
    using System.Collections.Generic;

    public class GetDestinationsPreviewQuery : IRequest<PagedDestinationResponse>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }

        public string OrderBy { get; set; }

        public string Query { get; set; }
    }
}