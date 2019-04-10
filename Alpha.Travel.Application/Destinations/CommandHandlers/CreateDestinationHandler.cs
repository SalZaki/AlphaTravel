namespace Alpha.Travel.Application.Destinations.CommandHandlers
{
    using Destinations.Queries;
    using Domain.Entities;
    using Persistence;
    using Destinations.Commands;
    using Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Models;

    public class CreateDestinationHandler : IRequestHandler<CreateDestination, DestinationPreviewDto>
    {
        private readonly AlphaTravelDbContext _context;
        private readonly IMediator _mediator;

        public CreateDestinationHandler(
            AlphaTravelDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<DestinationPreviewDto> Handle(CreateDestination request, CancellationToken cancellationToken)
        {
            int id = int.Parse(request.Id);

            var entity = new Destination
            {
                Id = id,
                Description = request.Description,
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };

            _context.Destinations.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await _mediator.Send(new GetDestinationPreviewQuery() { Id = entity.Id });
        }
    }
}