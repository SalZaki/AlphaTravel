namespace Alpha.Travel.Application.Customers.CommandHandlers
{
    using Customers.Commands;
    using Persistence;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer, Unit>
    {
        private readonly AlphaTravelDbContext _context;

        public UpdateCustomerHandler(AlphaTravelDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomer request, CancellationToken cancellationToken)
        {
            var entity = _context.Customers.SingleOrDefault(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new CustomerNotFoundException();
            }

            entity.Firstname = request.Firstname;
            entity.Email = request.Email;
            entity.ModifiedOn = request.ModifiedOn;
            entity.ModifiedBy = request.ModifiedBy;
            entity.Password = request.Password;
            entity.Surname = request.Surname;

            _context.Customers.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}