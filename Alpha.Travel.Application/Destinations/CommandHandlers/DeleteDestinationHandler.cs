namespace Alpha.Travel.Application.Destinations.CommandHandlers
{
    using Persistence;
    using Destinations.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using Exceptions;

    public class DeleteDestinationHandler : IRequestHandler<DeleteDestination>
    {
        private readonly AlphaTravelDbContext _context;
        private readonly IMediator _mediator;

        public DeleteDestinationHandler(
            AlphaTravelDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteDestination request, CancellationToken cancellationToken)
        {
            var id = int.Parse(request.Id);
            var entity = _context.Destinations.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new DestinationNotFoundException();
            }

            _context.Destinations.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}