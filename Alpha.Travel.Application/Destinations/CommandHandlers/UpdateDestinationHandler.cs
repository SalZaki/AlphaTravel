namespace Alpha.Travel.Application.Destinations.CommandHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Exceptions;
    using Persistence;
    using MediatR;

    public class UpdateDestinationHandler : IRequestHandler<UpdateDestination, Unit>
    {
        private readonly AlphaTravelDbContext _context;

        public UpdateDestinationHandler(AlphaTravelDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDestination request, CancellationToken cancellationToken)
        {
            var id = int.Parse(request.Id);
            var entity = _context.Destinations.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new DestinationNotFoundException();
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.ModifiedBy = request.ModifiedBy;
            entity.ModifiedOn = request.ModifiedOn;

            _context.Destinations.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}