namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Persistence;
    using Queries;
    using Models;

    public class GetDestinationPreviewQueryHandler : IRequestHandler<GetDestinationPreviewQuery, DestinationPreviewDto>
    {
        private readonly AlphaTravelDbContext _context;

        public GetDestinationPreviewQueryHandler(AlphaTravelDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<DestinationPreviewDto> Handle(GetDestinationPreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.Destinations.Select(DestinationPreviewDto.Projection).Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}