namespace Alpha.Travel.WebApi
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Alpha.Travel.WebApi.Models;
    using Alpha.Travel.Application.Common.Queries;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;

    public sealed class ResponseFactory : IResponseFactory
    {
        private readonly ILinkFactoryProvider _linkFactoryProvider;
        private readonly ILinkFactory _linkFactory;

        public ResponseFactory(ILinkFactoryProvider linkFactoryProvider)
        {
            _linkFactoryProvider = linkFactoryProvider ?? throw new ArgumentNullException(nameof(linkFactoryProvider));
            _linkFactory = _linkFactoryProvider.GetLinkFactory();
        }

        private List<LinkAttribute> GetLinkAttributes(Type type)
        {
            var methods = type
                .GetMethods()
                .Where(x => x.ReturnType == typeof(Task<IActionResult>))
                .ToList();

            var linkAttributes = new List<LinkAttribute>();

            methods.ForEach(m =>
            {
                var routeAttribute = m.GetCustomAttributes(typeof(RouteAttribute), false).SingleOrDefault() as RouteAttribute;
                var httpMethodAttribute = m.GetCustomAttributes(typeof(HttpMethodAttribute), false).SingleOrDefault() as HttpMethodAttribute;
                linkAttributes.Add(new LinkAttribute
                {
                    HttpMethodAttribute = httpMethodAttribute,
                    RouteAttribute = routeAttribute
                });
            });

            return linkAttributes;
        }

        public Response<T> CreateReponse<T>(T dto, Type type, ResponseStatus status, string version) where T : BaseModel
        {
            var linkAttributes = GetLinkAttributes(type);
            linkAttributes.ForEach(l => l.Id = dto.Id);
            var links = _linkFactory.CreateLinks(linkAttributes);
            dto.AddLinks(links);

            return new Response<T>
            {
                Data = dto,
                Status = status.ToString(),
                Version = version
            };
        }

        public PagedResponse<T> CreatePagedReponse<T>(IList<T> data,
            Type type,
            IPreviewQuery query,
            ResponseStatus status,
            string version) where T : BaseModel
        {
            var linkAttributes = GetLinkAttributes(type);

            data.ToList().ForEach(d =>
            {
                linkAttributes.ForEach(l => l.Id = d.Id);
                var links = _linkFactory.CreateLinks(linkAttributes);
                d.AddLinks(links);
            });

            return new PagedResponse<T>
            {
                Data = data,
                Pagination = new Pagination
                {
                    TotalRecords = data.Count,
                    PageCount = query.GetTotalPages(data.Count),
                    PageNumber = query.PageNumber,
                    HasNext = query.HasNext(data.Count),
                    HasPrevious = query.HasPrevious(),
                    PageSize = query.PageSize
                },
                Status = status.ToString(),
                Version = version,
                Metadata = new List<MetaData>()
            };
        }
    }
}