namespace Alpha.Travel.WebApi.Models
{
    public class Collection<T> : Resource
    {
        public T[] Data { get; set; }
    }
}