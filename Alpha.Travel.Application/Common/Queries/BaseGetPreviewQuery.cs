namespace Alpha.Travel.Application.Common.Queries
{
    using System;
    using System.Linq;

    public abstract class BaseGetPreviewQuery : IPreviewQuery
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public string Query { get; set; }

        public bool HasPrevious()
        {
            return PageNumber > 1;
        }

        public bool HasNext(int totalCount)
        {
            return (PageNumber < (int)GetTotalPages(totalCount));
        }

        public double GetTotalPages(int totalCount)
        {
            return Math.Ceiling(totalCount / (double)PageSize);
        }

        public bool HasQuery()
        {
            return !string.IsNullOrEmpty(Query);
        }

        public bool HasOrder()
        {
            return !string.IsNullOrEmpty(OrderBy);
        }

        public bool IsDescending()
        {
            if (!string.IsNullOrEmpty(OrderBy))
            {
                return OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            }
            return false;
        }
    }
}