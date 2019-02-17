namespace Alpha.Travel.Application.Destinations.Queries
{
    using Destinations.Models;
    using Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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