namespace Alpha.Travel.Application.Destinations.Models
{
    using Common.Models.Responses;
    using System.Collections.Generic;

    public class DestinationResponse : BaseResponse<DestinationPreviewDto>
    {

    }

    public class PagedDestinationResponse : BaseResponse<IReadOnlyCollection<DestinationPreviewDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => (PageIndex > 0);
        public bool HasNextPage => (PageIndex + 1 < TotalPages);
    }
}