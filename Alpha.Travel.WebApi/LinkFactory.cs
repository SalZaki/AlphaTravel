namespace Alpha.Travel.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Travel.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;

    public sealed class LinkFactory : ILinkFactory
    {
        private readonly IUrlHelper _urlHelper;

        public LinkFactory(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public IEnumerable<Link> CreateLinks(IList<LinkAttribute> linkAttributes)
        {
            var links = new List<Link>();

            linkAttributes.ToList().ForEach(a =>
            {
                var id = a.Id;
                var routeName = a.RouteAttribute?.Name;

                if (a.HttpMethodAttribute.TypeId == typeof(HttpGetAttribute))
                {
                    if (routeName.Contains("GetAll"))
                    {

                    }
                    else
                    {
                        links.Add(new SelfLink(_urlHelper.Link(routeName, new { id })));
                    }
                }

                if (a.HttpMethodAttribute.TypeId == typeof(HttpPostAttribute))
                {
                    links.Add(new PostLink(_urlHelper.Link(routeName, null)));
                }

                if (a.HttpMethodAttribute.TypeId == typeof(HttpPutAttribute))
                {
                    links.Add(new UpdateLink(_urlHelper.Link(routeName, new { id })));
                }

                if (a.HttpMethodAttribute.TypeId == typeof(HttpDeleteAttribute))
                {
                    links.Add(new DeleteLink(_urlHelper.Link(routeName, new { id })));
                }
            });

            return links;
        }
    }
}
