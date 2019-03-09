namespace Alpha.Travel.WebApi.Models.Responses
{
    using System.Collections.Generic;
    //    using Newtonsoft.Json;

    //    public class PagedResponse<T> : BaseResponse
    //    {
    //        [JsonProperty("href")]
    //        public string Href { get; set; }

    //        [JsonProperty("data")]
    //        public List<T> Data { get; set; }

    //        [JsonProperty("limit")]
    //        public int Limit { get; set; }

    //        [JsonProperty("next")]
    //        public string Next { get; set; }

    //        [JsonProperty("offset")]
    //        public int Offset { get; set; }

    //        [JsonProperty("previous")]
    //        public string Previous { get; set; }

    //        [JsonProperty("total")]
    //        public int Total { get; set; }

    //        public bool HasNextPage()
    //        {
    //            return Next != null;
    //        }

    //        public bool HasPreviousPage()
    //        {
    //            return Next != null;
    //        }
    //    }

    public class PagedResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Items { get; set; }

        public long TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
    
    //    public class PagedResults<T>
    //    {
    //        /// <summary>
    //        /// The page number this page represents.
    //        /// </summary>
    //        public int PageNumber { get; set; }

    //        /// <summary>
    //        /// The size of this page.
    //        /// </summary>
    //        public int PageSize { get; set; }

    //        /// <summary>
    //        /// The total number of pages available.
    //        /// </summary>
    //        public int TotalNumberOfPages { get; set; }

    //        /// <summary>
    //        /// The total number of records available.
    //        /// </summary>
    //        public int TotalNumberOfRecords { get; set; }

    //        /// <summary>
    //        /// The URL to the next page - if null, there are no more pages.
    //        /// </summary>
    //        public string NextPageUrl { get; set; }

    //        /// <summary>
    //        /// The records this page represents.
    //        /// </summary>
    //        public IEnumerable<T> Results { get; set; }
    //    }
}