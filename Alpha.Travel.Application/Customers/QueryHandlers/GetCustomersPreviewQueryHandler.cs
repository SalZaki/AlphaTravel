namespace Alpha.Travel.Application.Customers.QueryHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;

    using Queries;
    using Models;
    using Persistence;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Common.Handlers;
    using Domain.Entities;
    using Common.Models;
    using Common.Queries;

    public class GetCustomersPreviewQueryHandler : BaseValidationHandler<GetCustomersPreviewQuery, Common.Models.PagedResult<CustomerPreviewDto>>
    {
        public GetCustomersPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetCustomersPreviewQuery> validator)
            : base(context, validator) { }

        public override async Task<Common.Models.PagedResult<CustomerPreviewDto>> OnHandle(GetCustomersPreviewQuery request, CancellationToken cancellationToken)
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

            var customers = await query
                .Select(CustomerPreviewDto.Projection)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var response = new Common.Models.PagedResult<CustomerPreviewDto>
            {
                Data = customers,
                MetaData = new MetaData
                {
                    TotalRecords = count,
                    PageCount = request.GetTotalPages(count),
                    PageNumber = request.PageNumber,
                    HasNext = request.HasNext(count),
                    HasPrevious = request.HasPrevious(),
                    PageSize = request.PageSize
                }
            };

            return response;
        }
    }
}