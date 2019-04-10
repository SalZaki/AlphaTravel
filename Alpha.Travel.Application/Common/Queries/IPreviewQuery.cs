namespace Alpha.Travel.Application.Common.Queries
{
    public interface IPreviewQuery
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }

        string OrderBy { get; set; }

        string Query { get; set; }

        bool HasPrevious();

        bool HasNext(int totalCount);

        double GetTotalPages(int totalCount);

        bool HasQuery();

        bool HasOrder();

        bool IsDescending();
    }
}