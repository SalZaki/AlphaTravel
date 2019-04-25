namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;

    public sealed class PostLink : Link
    {
        public PostLink(string href) : base(href, "post", PostMethod) { }
    }
}
