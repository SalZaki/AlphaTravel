namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;
    using Queries;

    public class GetDestinationsPreviewQueryHandler : IRequestHandler<GetDestinationsPreviewQuery, List<DestinationPreviewDto>>
    {
        private readonly AlphaTravelDbContext _context;

        public GetDestinationsPreviewQueryHandler(AlphaTravelDbContext context)
        {
            _context = context;
        }

        public Task<List<DestinationPreviewDto>> Handle(GetDestinationsPreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.Destinations.Select(DestinationPreviewDto.Projection).ToListAsync(cancellationToken);
        }
    }
}