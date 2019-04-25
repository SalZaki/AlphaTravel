namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;

    public sealed class SelfLink : Link
    {
        public SelfLink(string href) : base(href, "self", GetMethod) { }
    }
}
