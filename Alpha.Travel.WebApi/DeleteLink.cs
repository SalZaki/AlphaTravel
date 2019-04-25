namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;

    public sealed class DeleteLink : Link
    {
        public DeleteLink(string href) : base(href, "delete", DeleteMethod) { }
    }
}
