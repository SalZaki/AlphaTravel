namespace Alpha.Travel.Application.Customers.QueryHandlers
{
    using Queries;
    using Models;
    using Persistence;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Handlers;
    using Domain.Entities;
    using System.Linq.Dynamic.Core;

    public class GetCustomersPreviewQueryHandler : ValidationHandler<GetCustomersPreviewQuery, PagedResults<CustomerPreviewDto>>
    {
        public GetCustomersPreviewQueryHandler(AlphaTravelDbContext context, IValidator<GetCustomersPreviewQuery> validator) : base(context, validator) { }

        public override async Task<PagedResults<CustomerPreviewDto>> OnHandle(GetCustomersPreviewQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Customers as IQueryable<Customer>;

            if (request.HasOrder())
            {
                query = query.OrderBy(request.OrderBy, request.IsDescending());
            }

            if (request.HasQuery())
            {
                query = query
                    .Where(x => x.Firstname.ToLowerInvariant()
                    .Contains(request.Query.ToLowerInvariant()));
            }

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Select(CustomerPreviewDto.Projection)
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToArrayAsync(cancellationToken);

            return new PagedResults<CustomerPreviewDto>
            {
                Items = items,
                Count = count
            };
        }
    }
}