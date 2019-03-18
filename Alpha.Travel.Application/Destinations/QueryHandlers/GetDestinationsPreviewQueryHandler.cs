
namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;

    using Models;
    using Persistence;
    using Queries;
    using Microsoft.EntityFrameworkCore;
    using FluentValidation;
    using Common.Handlers;
    using Domain.Entities;

    public class GetDestinationsPreviewQueryHandler : ValidationHandler<GetDestinationsPreviewQuery, PagedResults<DestinationPreviewDto>>
    {
        public GetDestinationsPreviewQueryHandler(AlphaTravelDbContext context, IValidator<GetDestinationsPreviewQuery> validator) : base(context, validator) { }

        public override async Task<PagedResults<DestinationPreviewDto>> OnHandle(GetDestinationsPreviewQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Destinations as IQueryable<Destination>;

            if (request.HasOrder())
            {
                query = query
                    .OrderBy(request.OrderBy, request.IsDescending());
            }

            if (request.HasQuery())
            {
                query = query
                    .Where(x => x.Name.ToLowerInvariant()
                    .Contains(request.Query.ToLowerInvariant()));
            }

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Select(DestinationPreviewDto.Projection)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToArrayAsync(cancellationToken);

            return new PagedResults<DestinationPreviewDto>
            {
                Items = items,
                Count = count
            };
        }
    }
}