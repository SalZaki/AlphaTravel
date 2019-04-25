namespace Alpha.Travel.WebApi
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;

    public sealed class LinkAttribute
    {
        public int Id { get; set; }
        public HttpMethodAttribute HttpMethodAttribute { get; set; }
        public RouteAttribute RouteAttribute { get; set; }
    }
}
