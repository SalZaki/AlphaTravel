namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;
    using System.Collections.Generic;

    public interface ILinkFactory
    {
        IEnumerable<Link> CreateLinks(IList<LinkAttribute> linkAttributes);
    }
}