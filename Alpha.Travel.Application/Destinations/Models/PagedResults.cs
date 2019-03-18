namespace Alpha.Travel.Application.Models
{
    using System.Collections.Generic;

    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }
    }
}