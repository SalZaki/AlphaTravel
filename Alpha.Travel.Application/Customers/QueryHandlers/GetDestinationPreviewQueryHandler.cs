namespace Alpha.Travel.Application.Customers.QueryHandlers
{
    using Queries;
    using Application.Models;
    using Persistence;
    using FluentValidation;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Handlers;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class GetDestinationPreviewQueryHandler : ValidationHandler<GetCustomerPreviewQuery, CustomerPreviewDto>
    {
        public GetDestinationPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetCustomerPreviewQuery> validator)
            : base(context, validator) { }

        public override async Task<CustomerPreviewDto> OnHandle(GetCustomerPreviewQuery request, CancellationToken cancellationToken)
        {
            int id = int.Parse(request.Id);

            var response = await Context.Customers
                .Select(CustomerPreviewDto.Projection)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

            if (response == null)
            {
                throw new CustomerNotFoundException();
            }

            return response;
        }
    }
}