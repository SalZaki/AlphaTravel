namespace Alpha.Travel.Application.Common.Models.Requests
{
    public class PagingOptions
    {
        public PagingOptions()
        {
            PageSize = 10;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }

        public string OrderBy { get; set; } = "Name";
    }
}  