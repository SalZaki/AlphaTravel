namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;

    public sealed class UpdateLink : Link
    {
        public UpdateLink(string href) : base(href, "update", UpdateMethod) { }
    }
}
