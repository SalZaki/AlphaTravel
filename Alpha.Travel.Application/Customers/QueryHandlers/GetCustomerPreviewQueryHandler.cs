namespace Alpha.Travel.Application.Customers.QueryHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;

    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    using Persistence;
    using Queries;
    using Models;
    using Exceptions;
    using Common.Handlers;
    using Domain.Entities;

    public class GetCustomerPreviewQueryHandler : BaseValidationHandler<GetCustomerPreviewQuery, CustomerPreviewDto>
    {
        private readonly IMapper _mapper;

        public GetCustomerPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetCustomerPreviewQuery> validator,
            IMapper mapper)
            : base(context, validator)
        {
            _mapper = mapper;
        }

        public override async Task<CustomerPreviewDto> OnHandle(GetCustomerPreviewQuery request, CancellationToken cancellationToken)
        {
            var customer = null as Customer;
            try
            {
                customer = await Context.Customers.SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }

            if (customer == null)
            {
                throw new CustomerNotFoundException();
            }

            return _mapper.Map<CustomerPreviewDto>(customer);
        }
    }
}