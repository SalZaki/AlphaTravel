﻿namespace Alpha.Travel.Application.Customers.CommandHandlers
{
    using Domain.Entities;
    using Persistence;
    using Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Customers.Commands;
    using Customers.Queries;
 
    public class CreateCustomerHandler : IRequestHandler<CreateCustomer, CustomerPreviewDto>
    {
        private readonly AlphaTravelDbContext _context;
        private readonly IMediator _mediator;

        public CreateCustomerHandler(
            AlphaTravelDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CustomerPreviewDto> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                Id = request.Id,
                Email = request.Email,
                Firstname = request.Firstname,
                Surname = request.Surname,
                Password = request.Password,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };

            _context.Customers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await _mediator.Send(new GetCustomerPreviewQuery { Id = entity.Id });
        }
    }
}