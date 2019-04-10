namespace Alpha.Travel.Application.Customers.QueryHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using Queries;
    using Models;
    using Persistence;
    using FluentValidation;
    using Common.Handlers;
    using Domain.Entities;

    public class GetCustomersPreviewQueryHandler : BaseValidationHandler<GetCustomersPreviewQuery, IList<CustomerPreviewDto>>
    {
        private readonly IMapper _mapper;

        public GetCustomersPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetCustomersPreviewQuery> validator,
            IMapper mapper)
            : base(context, validator)
        {
            _mapper = mapper;
        }

        public override async Task<IList<CustomerPreviewDto>> OnHandle(GetCustomersPreviewQuery request, CancellationToken cancellationToken)
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
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IList<CustomerPreviewDto>>(customers);
        }
    }
}