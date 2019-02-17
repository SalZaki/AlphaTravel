namespace Alpha.Travel.Application.Destinations.Queries
{
    using MediatR;
    using Destinations.Models;
    using System.Threading;
    using System.Threading.Tasks;
    using Persistence;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class GetDestinationPreviewQueryHandler : IRequestHandler<GetDestinationPreviewQuery, DestinationPreviewDto>
    {
        private readonly AlphaTravelDbContext _context;

        public GetDestinationPreviewQueryHandler(AlphaTravelDbContext context)
        {
            _context = context;
        }

        public Task<DestinationPreviewDto> Handle(GetDestinationPreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.Destinations.Select(DestinationPreviewDto.Projection).Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}