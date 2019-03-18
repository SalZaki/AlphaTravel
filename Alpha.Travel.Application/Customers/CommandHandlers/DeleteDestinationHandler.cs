namespace Alpha.Travel.Application.Customers.CommandHandlers
{
    using Persistence;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Exceptions;

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer>
    {
        private readonly AlphaTravelDbContext _context;
        private readonly IMediator _mediator;

        public DeleteCustomerHandler(
            AlphaTravelDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteCustomer request, CancellationToken cancellationToken)
        {
            var id = int.Parse(request.Id);
            var entity = _context.Destinations.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new CustomerNotFoundException();
            }

            _context.Destinations.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}