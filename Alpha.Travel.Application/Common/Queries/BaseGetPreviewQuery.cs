namespace Alpha.Travel.Application.Common.Queries
{
    public abstract class BaseGetPreviewQuery
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public string Query { get; set; }
    }
}
