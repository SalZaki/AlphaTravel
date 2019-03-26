namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;

    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    using Persistence;
    using Queries;
    using Models;
    using Common.Handlers;
    using Common.Models;
    using Common.Queries;
    using Domain.Entities;

    public class GetDestinationsPreviewQueryHandler : BaseValidationHandler<GetDestinationsPreviewQuery, Common.Models.PagedResult<DestinationPreviewDto>>
    {
        public GetDestinationsPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetDestinationsPreviewQuery> validator)
            : base(context, validator) { }

        public override async Task<Common.Models.PagedResult<DestinationPreviewDto>> OnHandle(GetDestinationsPreviewQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Destinations as IQueryable<Destination>;

            if (request.HasOrder())
            {
                query = query.OrderBy(request.OrderBy, request.IsDescending());
            }

            if (request.HasQuery())
            {
                query = query
                    .Where(x => x.Name.ToLowerInvariant()
                    .Contains(request.Query.ToLowerInvariant()));
            }

            var count = await query.CountAsync(cancellationToken);

            var destinations = await query
                .Select(DestinationPreviewDto.Projection)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var response = new Common.Models.PagedResult<DestinationPreviewDto>
            {
                Data = destinations,
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