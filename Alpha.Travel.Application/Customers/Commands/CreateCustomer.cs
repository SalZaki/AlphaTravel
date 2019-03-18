namespace Alpha.Travel.Application.Customers.Commands
{
    using Models;
    using MediatR;
    using System;

    public class CreateCustomer : IRequest<CustomerPreviewDto>
    {
        public string Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}