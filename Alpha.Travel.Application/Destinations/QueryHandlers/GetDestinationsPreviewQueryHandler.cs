namespace Alpha.Travel.Application.Destinations.QueryHandlers
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
    using Common.Queries;
    using System.Collections.Generic;
    using AutoMapper;

    public class GetDestinationsPreviewQueryHandler : BaseValidationHandler<GetDestinationsPreviewQuery, IList<DestinationPreviewDto>>
    {
        private readonly IMapper _mapper;

        public GetDestinationsPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetDestinationsPreviewQuery> validator,
            IMapper mapper)
            : base(context, validator)
        {
            _mapper = mapper;
        }

        public override async Task<IList<DestinationPreviewDto>> OnHandle(GetDestinationsPreviewQuery request, CancellationToken cancellationToken)
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
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IList<DestinationPreviewDto>>(destinations);
        }
    }
}